# conding=utf8
import datetime
import os
import re
from urllib.parse import urljoin


def creat_xml(filename, url_list):  # create sitemap
    header = '<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">\n'
    with open(filename, 'w', encoding='utf-8') as file:
        file.writelines(header)
        for url in url_list:
            times = datetime.datetime.now().strftime("%Y-%m-%dT%H:%M:%S+00:00")
            urls = re.sub(r"&", "&amp;", url)  # encode &
            # change it on demand
            ment = "  <url>\n    <loc>%s</loc>\n    <lastmod>%s</lastmod>\n    <changefreq>always</changefreq>\n    <priority>0.8</priority>\n  </url>\n" % (
                urls, times)
            file.writelines(ment)
        last = "</urlset>"
        file.writelines(last)
        file.close()


def create_robots(filename, domain, robotSet=None):
    if (robotSet is None):
        robotSet = set([r"User-Agent: *",
                       r"Allow: /",
                        r"Sitemap: "+urljoin(domain, "sitemap.xml"),
                        r"Disallow: /getting-started/getting-started/*",
                        r"Disallow: /introduction/introduction/*",
                        r"Disallow: /about/about/*",
                        r"Disallow: /features/features/*",
                        r"Disallow: /stylesandanimations/stylesandanimations/*",
                        r"Disallow: /components/components/*",
                        r"Disallow: /api/api/*",
                        r"Disallow: /_host",
                        r"Disallow: /_host/*",
                        r"Disallow: /**/related-agreements"])
    with open(filename, 'w', encoding='utf-8') as file:
        file.writelines('\n'.join(robotSet))
        file.close()


base_dir = os.path.join(
    os.getcwd(), r'docs/Masa.Docs.WebAssembly/bin/Release/net8.0/publish/wwwroot')

#base_dir = r"D:\Source\Stack\src\MASA.Blazor\docs\Masa.Docs.WebAssembly\bin\Release\net7.0\publish\wwwroot"
print(base_dir)
print(os.getcwd())

projects = {"stack": r"_content/Masa.Stack.Docs/pages",
            "framework": r"_content/Masa.Framework.Docs/pages",
            "blazor": r"_content/Masa.Blazor.Docs/pages"}

docPaths = set()
for key in projects:
    projectDir = os.path.join(base_dir, projects[key])
    print(projectDir)
    g = os.walk(projectDir)
    for path, dir_list, file_list in g:
        for file_name in file_list:
            relative_path = path.replace(
                projectDir, os.sep+key).replace(os.sep, '/')
            if (file_name.endswith(".md")):
                # print(relative_path)
                docPaths.add(relative_path)
# print(docPaths)
print(len(docPaths))

docDomain = os.environ.get('DOC_DOMAIN')
if (docDomain is None):
    docDomain = "https://blazor.masastack.com"

urls = list()
for docPath in docPaths:
    urls.append(urljoin(docDomain, docPath))

print(len(urls))

sitemapPath = os.path.join(base_dir, "sitemap.xml")
print(sitemapPath)
creat_xml(sitemapPath, urls)

robotsPath = os.path.join(base_dir, "robots.txt")
create_robots(robotsPath, docDomain)

# with open(sitemapPath, 'r', encoding='utf-8') as file:
#     print(file.readlines())
