using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Docs.Shared.Examples.forms
{
    public class Usage : Masa.Docs.Shared.Components.Usage
    {
        public Usage() : base(typeof(MForm)) { }

        protected override Dictionary<string, object>? GenAdditionalParameters()
        {
            return new Dictionary<string, object>()
            {
              { nameof(MForm.Model), _form },
              { nameof(MForm.EnableValidation), true },
                { nameof(MForm.ChildContent), new RenderFragment<FormContext>(context => builder =>
                {
                    builder.OpenComponent<Basic>(0);
                    builder.CloseComponent();
                })},
            };
        }

        public class Form
        {
            [Required(ErrorMessage = "Name is required")]
            [MaxLength(10, ErrorMessage = "Name must be less than 10 characters")]
            public string Firstname { get; set; }

            [Required(ErrorMessage = "Name is required")]
            [MaxLength(10, ErrorMessage = "Name must be less than 10 characters")]
            public string Lastname { get; set; }

            [Required(ErrorMessage = "E-mail is required")]
            [EmailAddress(ErrorMessage = "E-mail must be valid")]
            public string Email { get; set; }
        }

        private Form _form = new();
    }
}
