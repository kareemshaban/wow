#pragma checksum "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\level\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "71b35daaa82150081b3c377b3b21f66aae1bc4b9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_level_Edit), @"mvc.1.0.view", @"/Views/level/Edit.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/level/Edit.cshtml", typeof(AspNetCore.Views_level_Edit))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"71b35daaa82150081b3c377b3b21f66aae1bc4b9", @"/Views/level/Edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f65067f5168b29dd9d623309be4e654a1b4da021", @"/Views/_ViewImports.cshtml")]
    public class Views_level_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Level>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("imgShow"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("thumbnail"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width: 302px;margin-top: 30px;height: 261px;border: 1px solid #7d7d7d;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\level\Edit.cshtml"
  
    ViewData["Title"] = "?????????? ??????????";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(108, 775, true);
            WriteLiteral(@"<div class=""col-sm-12"" style=""margin-top:40px"">
    <div class=""card"">
        <div class=""card-header"">
            <h5>?????????? ??????????</h5>
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
        ");
            EndContext();
            BeginContext(883, 2687, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "71b35daaa82150081b3c377b3b21f66aae1bc4b96781", async() => {
                BeginContext(951, 162, true);
                WriteLiteral("\r\n            <div class=\"card-body\">\r\n                <div class=\"row\">\r\n                    <div class=\"col-md-8\">\r\n                        <input type=\"hidden\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1113, "\"", 1130, 1);
#line 25 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\level\Edit.cshtml"
WriteAttributeValue("", 1121, Model.Id, 1121, 9, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1131, 327, true);
                WriteLiteral(@" name=""Id"" />
                        <div class=""form-group row"">
                            <div class=""col-md-12"">
                                <label class=""control-label"" style=""font-weight:bold;float:right"">?????? ??????????</label>
                                <input type=""text"" name=""LevelName"" class=""form-control""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1458, "\"", 1482, 1);
#line 29 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\level\Edit.cshtml"
WriteAttributeValue("", 1466, Model.LevelName, 1466, 16, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1483, 398, true);
                WriteLiteral(@" />
                            </div>
                            </div>

                            <div class=""form-group row"">
                                <div class=""col-md-6"">
                                    <label class=""control-label"" style=""font-weight:bold;float:right"">??????????</label>
                                    <input type=""text"" name=""Color"" class=""form-control""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1881, "\"", 1901, 1);
#line 36 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\level\Edit.cshtml"
WriteAttributeValue("", 1889, Model.Color, 1889, 12, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1902, 327, true);
                WriteLiteral(@"/>
                                </div>
                                <div class=""col-md-6"">
                                    <label class=""control-label"" style=""font-weight:bold;float:right"">?????? ?????????????? ??????????????</label>
                                    <input type=""text"" name=""GiftSendCount"" class=""form-control""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2229, "\"", 2257, 1);
#line 40 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\level\Edit.cshtml"
WriteAttributeValue("", 2237, Model.GiftSendCount, 2237, 20, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2258, 380, true);
                WriteLiteral(@" />
                                </div>
                            </div>
                            <div class=""form-group row"">
                                <div class=""col-md-12"">
                                    <label class=""control-label"" style=""font-weight:bold;float:right"">?????? ???????? ??????????????</label>
                                    <input type=""hidden""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2638, "\"", 2659, 1);
#line 46 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\level\Edit.cshtml"
WriteAttributeValue("", 2646, Model.ImgUrl, 2646, 13, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2660, 673, true);
                WriteLiteral(@" name=""ImgUrl"" />
                                    <input type=""file"" name=""Img"" id=""imgUploader"" class=""form-control"" style=""margin-top:25px"" />
                                </div>
                            </div>
                            <div class=""form-group row"">
                                <div class=""col-md-12"">
                                    <input type=""submit"" value=""?????? ??????????????????"" class=""btn btn-success btn-block"" id=""btnSave"" />
                                </div>
                            </div>

                        </div>
                    <div class=""col-md-4"">                       
                        ");
                EndContext();
                BeginContext(3333, 148, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "71b35daaa82150081b3c377b3b21f66aae1bc4b911827", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 3356, "~/LevelImg/", 3356, 11, true);
#line 58 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\level\Edit.cshtml"
AddHtmlAttributeValue("", 3367, Model.ImgUrl, 3367, 13, false);

#line default
#line hidden
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3481, 82, true);
                WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3570, 22, true);
            WriteLiteral("\r\n    </div>\r\n</div>\r\n");
            EndContext();
            DefineSection("scripts", async() => {
                BeginContext(3609, 1268, true);
                WriteLiteral(@"
    <script>
        $(document).ready(function () {
            $(""#imgUploader"").change(function () {
                var img = $(""#imgUploader"").get(0).files[0];
                if (img != """") {
                    $(""#btnSave"").css(""display"", ""block"");                   
                    //>>>>>>Size
                    var fileSize = img.size / 1024 / 1024;
                    if (fileSize > 10) {
                        alert(""???????????? ?????? ???? ???? ???????? ???? 10  ????????"");
                        $(""#imgUploader"").val('')
                        $(""#imgShow"").css(""display"", ""none"");
                        return false;
                    }
                    //>>>>>>>>Show
                    var reader = new FileReader;
                    var image = new Image;
                    reader.readAsDataURL(img);
                    reader.onload = function (_file) {
                        image.src = _file.target.result;
                        image.onload = function () {
             ");
                WriteLiteral("               $(\"#imgShow\").attr(\'src\', _file.target.result);\r\n                            $(\"#imgShow\").css(\"display\", \"block\");\r\n                        }\r\n                    }\r\n                }\r\n            })\r\n        })\r\n    </script>\r\n");
                EndContext();
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Level> Html { get; private set; }
    }
}
#pragma warning restore 1591
