using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Renderers;
using Markdig.Syntax;
using Masa.Blazor.Doc.Models;
using Masa.Blazor.Doc.Models.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Masa.Blazor.Doc.CLI.Wrappers
{
    public class DocWrapper
    {
        public static (DemoFrontMatter meta, string desc, Dictionary<string, string> others)
            ParseDemoDoc(string input)
        {
            var pipeline = new MarkdownPipelineBuilder()
                .UseYamlFrontMatter()
                .UsePipeTables()
                .Build();

            var document = Markdown.Parse(input, pipeline);
            var yamlBlock = document.Descendants<YamlFrontMatterBlock>().FirstOrDefault();

            DemoFrontMatter meta = null;
            if (yamlBlock != null)
            {
                var yaml = input.Substring(yamlBlock.Span.Start, yamlBlock.Span.Length).Trim('-');
                var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
                meta = deserializer.Deserialize<DemoFrontMatter>(yaml);
            }

            var currentH2 = string.Empty;
            Dictionary<string, string> h2Sections = new();

            for (var i = yamlBlock?.Line ?? 0; i < document.Count; i++)
            {
                var block = document[i];
                if (block is YamlFrontMatterBlock)
                    continue;

                if (block is HeadingBlock heading && heading.Level == 2)
                {
                    currentH2 = heading.Inline.FirstChild.ToString();
                }

                using var writer = new StringWriter();
                var renderer = new HtmlRenderer(writer);
                pipeline.Setup(renderer);

                var blockHtml = renderer.Render(block);

                if (h2Sections.ContainsKey(currentH2))
                {
                    h2Sections[currentH2] += blockHtml;
                }
                else
                {
                    h2Sections[currentH2] = blockHtml.ToString().RemoveTag("h2");
                }
            }

            if (h2Sections.TryGetValue(string.Empty, out var desc))
            {
                h2Sections.Remove(string.Empty);
            }

            return (meta, desc, h2Sections);
        }

        public static (DescriptionYaml Meta, string Style, Dictionary<string, string> Descriptions) ParseDescription(string input)
        {
            var pipeline = new MarkdownPipelineBuilder()
                .UseYamlFrontMatter()
                .Build();

            var document = Markdown.Parse(input, pipeline);
            var yamlBlock = document.Descendants<YamlFrontMatterBlock>().FirstOrDefault();

            DescriptionYaml meta = null;
            if (yamlBlock != null)
            {
                var yaml = input.Substring(yamlBlock.Span.Start, yamlBlock.Span.Length).Trim('-');
                meta = Deserializer.FromValueDeserializer(new DeserializerBuilder()
                        .WithNamingConvention(CamelCaseNamingConvention.Instance).BuildValueDeserializer())
                    .Deserialize<DescriptionYaml>(yaml);
            }

            var isAfterEnHeading = false;
            var isStyleBlock = false;
            var isCodeBlock = false;

            var zhPart = "";
            var enPart = "";
            var stylePart = "";
            var codePart = "";

            for (var i = yamlBlock?.Line ?? 0; i < document.Count; i++)
            {
                var block = document[i];
                if (block is YamlFrontMatterBlock)
                    continue;

                if (block is HeadingBlock heading && heading.Level == 2 && heading.Inline.FirstChild.ToString() == "en-US")
                {
                    isAfterEnHeading = true;
                }

                if (block is CodeBlock)
                {
                    isCodeBlock = true;
                }

                if (block is HtmlBlock htmlBlock && htmlBlock.Type == HtmlBlockType.ScriptPreOrStyle)
                {
                    isStyleBlock = true;
                }

                if (block is HeadingBlock h && h.Level == 2)
                    continue;

                using var writer = new StringWriter();
                var renderer = new HtmlRenderer(writer);

                var blockHtml = renderer.Render(block);

                if (!isAfterEnHeading)
                {
                    zhPart += blockHtml;
                }
                else if (isStyleBlock)
                {
                    stylePart += blockHtml;
                }
                else if (isCodeBlock)
                {
                    codePart += blockHtml;
                }
                else
                {
                    enPart += blockHtml;
                }
            }

            stylePart = stylePart.Replace("<style>", "").Replace("</style>", "");
            return (meta, stylePart, new Dictionary<string, string>() { ["zh-CN"] = zhPart, ["en-US"] = enPart });
        }

        public static Dictionary<string, string> ParseHeader(string input)
        {
            var pipeline = new MarkdownPipelineBuilder()
                .UseYamlFrontMatter()
                .Build();

            var writer = new StringWriter();
            var renderer = new HtmlRenderer(writer);
            pipeline.Setup(renderer);

            var document = Markdown.Parse(input, pipeline);
            var yamlBlock = document.Descendants<YamlFrontMatterBlock>().FirstOrDefault();

            Dictionary<string, string> meta = null;
            if (yamlBlock != null)
            {
                var yaml = input.Substring(yamlBlock.Span.Start, yamlBlock.Span.Length).Trim('-');
                meta = new Deserializer().Deserialize<Dictionary<string, string>>(yaml);
            }

            return meta;
        }

        public static IEnumerable<string> ParseTitle(string input)
        {
            input = input.Trim(' ', '\r', '\n');
            var pipeline = new MarkdownPipelineBuilder()
                .UseYamlFrontMatter()
                .UsePipeTables()
                .Build();

            StringWriter writer = new StringWriter();
            var renderer = new HtmlRenderer(writer);
            pipeline.Setup(renderer);

            MarkdownDocument document = Markdown.Parse(input, pipeline);
            var yamlBlock = document.Descendants<YamlFrontMatterBlock>().FirstOrDefault();
            var title = string.Empty;
            var order = 0;
            if (yamlBlock != null)
            {
                var yaml = input.Substring(yamlBlock.Span.Start, yamlBlock.Span.Length).Trim('-');
                var meta = new Deserializer().Deserialize<Dictionary<string, string>>(yaml);
                title = meta["title"];
                order = int.TryParse(meta["order"], out var o) ? o : 0;
            }

            renderer.Render(document);
            writer.Flush();
            var html = writer.ToString();

            var matches = Regex.Matches(html, "<h2>(?<title>.*)<\\/h2>");
            return matches.Select(r => r.Groups["title"].ToString());
        }

        public static (int order, string title, string html) ParseDocs(string input)
        {
            input = input.Trim(' ', '\r', '\n');
            var pipeline = new MarkdownPipelineBuilder()
                .UseYamlFrontMatter()
                .UsePipeTables()
                .Build();

            StringWriter writer = new StringWriter();
            var renderer = new HtmlRenderer(writer);
            pipeline.Setup(renderer);

            MarkdownDocument document = Markdown.Parse(input, pipeline);
            var yamlBlock = document.Descendants<YamlFrontMatterBlock>().FirstOrDefault();
            var title = string.Empty;
            var order = 0;
            if (yamlBlock != null)
            {
                var yaml = input.Substring(yamlBlock.Span.Start, yamlBlock.Span.Length).Trim('-');
                var meta = new Deserializer().Deserialize<Dictionary<string, string>>(yaml);
                title = meta["title"];
                order = int.TryParse(meta["order"], out var o) ? o : 0;
            }

            renderer.Render(document);
            writer.Flush();
            var html = writer.ToString();

            html = $"<h1>{title}</h1>\n" + html;

            var h1Class = "\"m-heading text-h3 text-sm-h3 mb-2\"";
            ;
            var h2Class = "\"m-heading text-h4 text-sm-h4 mb-3\"";
            var aClass = "\"text-decoration-none text-right text-md-left\"";
            var divClass = "\"mb-8\"";

            html = Regex.Replace(html, "<h2>(?<title>.*)<\\/h2>", m =>
                $@"
            </section><section id={m.Groups["title"].ToString().HashSection()}><h2>{m.Groups["title"]}</h2>");

            html = new Regex("<\\/section>").Replace(html, $"<div class={divClass}>&nbsp;</div>", 1);

            html = Regex.Replace(html, "<h(?<n>1|2)>(?<title>.*)<\\/h(1|2)>", m => m.Groups["n"].ToString() == "1"
                ? $@"
                <h1 class={h1Class}>
                    <a class={aClass}>#</a>
                    {m.Groups["title"]}
                </h1>"
                : $@"
                <h2 class={h2Class}>
                    <a class={aClass}>#</a>
                    {m.Groups["title"]}
                </h2>");

            //var preClass = "\"app-code overflow-hidden m-sheet m-sheet--outlined theme--light rounded grey lighten-5\"";
            //html = Regex.Replace(html, "(?<content><pre>[\\s\\S.]*<\\/pre>)", m => $@"
            //    <div class={preClass}>
            //        {m.Groups["content"]}
            //    </div>
            //");

            html = $"<section id=\"{title.HashSection()}\">{html}</section>";

            return (order, title, html);
        }
    }

    public class DescriptionYaml
    {
        public decimal Order { get; set; }

        public int? Iframe { get; set; }

        public Dictionary<string, string> Title { get; set; }

        public bool Debug { get; set; }

        public bool? Docs { get; set; }
    }
}