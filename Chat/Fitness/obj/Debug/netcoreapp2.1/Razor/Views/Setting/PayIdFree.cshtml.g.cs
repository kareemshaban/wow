#pragma checksum "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Setting\PayIdFree.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e9a5392fee3165cc39f6bdd5ecf6620514370a75"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Setting_PayIdFree), @"mvc.1.0.view", @"/Views/Setting/PayIdFree.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Setting/PayIdFree.cshtml", typeof(AspNetCore.Views_Setting_PayIdFree))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\_ViewImports.cshtml"
using Fitness;

#line default
#line hidden
#line 2 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\_ViewImports.cshtml"
using Fitness.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e9a5392fee3165cc39f6bdd5ecf6620514370a75", @"/Views/Setting/PayIdFree.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f65067f5168b29dd9d623309be4e654a1b4da021", @"/Views/_ViewImports.cshtml")]
    public class Views_Setting_PayIdFree : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SearchUserIdFree", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "PayIdFree", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Setting\PayIdFree.cshtml"
  
    ViewData["Title"] = "???????? ???????????? ??????????????";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(104, 880, true);
            WriteLiteral(@"<div class=""col-sm-12"" style=""margin-top:40px"">
    <div class=""card"">
        <div class=""card-header"">
            <h5>???????? ???????????? ??????????????</h5>
            <div class=""card-header-right"">
                <ul class=""list-unstyled card-option"">
                    <li><i class=""icofont icofont-simple-left""></i></li>
                    <li><i class=""view-html fa fa-code""></i></li>
                    <li><i class=""icofont icofont-maximize full-card""></i></li>
                    <li><i class=""icofont icofont-minus minimize-card""></i></li>
                    <li><i class=""icofont icofont-refresh reload-card""></i></li>
                    <li><i class=""icofont icofont-error close-card""></i></li>
                </ul>
            </div>
        </div>
        <div class=""card-body"">
            <div class=""row"">
                <div class=""col-md-12"">
");
            EndContext();
#line 24 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Setting\PayIdFree.cshtml"
                     if (ViewBag.data != null)
                    {

#line default
#line hidden
            BeginContext(1055, 185, true);
            WriteLiteral("                        <div class=\"form-group row text-center\">\r\n                            <input readonly class=\"alert alert-danger col-md-12 text-center\" style=\"border-radius:25px\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1240, "\"", 1261, 1);
#line 27 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Setting\PayIdFree.cshtml"
WriteAttributeValue("", 1248, ViewBag.data, 1248, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1262, 37, true);
            WriteLiteral(" />\r\n                        </div>\r\n");
            EndContext();
#line 29 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Setting\PayIdFree.cshtml"
                    }

#line default
#line hidden
            BeginContext(1322, 20, true);
            WriteLiteral("                    ");
            EndContext();
            BeginContext(1342, 779, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e9a5392fee3165cc39f6bdd5ecf6620514370a757089", async() => {
                BeginContext(1392, 360, true);
                WriteLiteral(@"
                        <div class=""form-group row"">
                            <div class=""col-md-9"">
                                <label class=""control-label"" style=""font-weight:bold;float:right"">???????? ???????????????? ???????????? ?????????? ??????</label>
                                <input type=""number"" name=""UserId"" class=""form-control"" style=""border-radius:25px""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1752, "\"", 1777, 1);
#line 34 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Setting\PayIdFree.cshtml"
WriteAttributeValue("", 1760, ViewBag.searchId, 1760, 17, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1778, 336, true);
                WriteLiteral(@" />
                            </div>
                            <div class=""col-md-3"">
                                <button type=""submit"" class=""btn btn-primary btn-block"" style=""margin-top:25px;border-radius:25px"">?????? ???? ????????????</button>
                            </div>
                        </div>
                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2121, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 41 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Setting\PayIdFree.cshtml"
                     if (ViewBag.searchId != null)
                       {

#line default
#line hidden
            BeginContext(2201, 24, true);
            WriteLiteral("                        ");
            EndContext();
            BeginContext(2225, 1154, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e9a5392fee3165cc39f6bdd5ecf6620514370a7510502", async() => {
                BeginContext(2268, 71, true);
                WriteLiteral("\r\n                            <input type=\"number\" name=\"UserId\" hidden");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2339, "\"", 2364, 1);
#line 44 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Setting\PayIdFree.cshtml"
WriteAttributeValue("", 2347, ViewBag.searchId, 2347, 17, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2365, 712, true);
                WriteLiteral(@" />
                            <div class=""form-group row"" style=""padding: 9px 0px;"">
                                <div class=""col-md-9"">
                                    <label class=""control-label"" style=""font-weight:bold;float:right;margin-right:15px"">???????? ???????????????? ????????????</label>
                                    <input type=""number"" name=""IdNow"" class=""form-control"" style=""border-radius:25px"" />
                                </div>
                                <div class=""col-md-3"">
                                    <input type=""submit"" class=""btn btn-secondary btn-block"" style=""margin-top:26px;border-radius:25px"" value=""???????? ??????????"" />
                                </div>
");
                EndContext();
                BeginContext(3312, 60, true);
                WriteLiteral("                            </div>\r\n                        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3379, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 58 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Setting\PayIdFree.cshtml"
                    }

#line default
#line hidden
            BeginContext(3404, 82, true);
            WriteLiteral("                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
