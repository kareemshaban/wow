#pragma checksum "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Product\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d918afd06991f45f575cc524660c634984e0e644"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product_Edit), @"mvc.1.0.view", @"/Views/Product/Edit.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Product/Edit.cshtml", typeof(AspNetCore.Views_Product_Edit))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d918afd06991f45f575cc524660c634984e0e644", @"/Views/Product/Edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f65067f5168b29dd9d623309be4e654a1b4da021", @"/Views/_ViewImports.cshtml")]
    public class Views_Product_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Product>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "CategoryId", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("CategoryId"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("imgShow"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("thumbnail"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:304px;margin-top:20px;height: 270px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Product\Edit.cshtml"
  
    ViewData["Title"] = "?????????? ??????????";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(110, 777, true);
            WriteLiteral(@"
<div class=""col-sm-12"" style=""margin-top:40px"">
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
            BeginContext(887, 2299, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d918afd06991f45f575cc524660c634984e0e6447903", async() => {
                BeginContext(955, 162, true);
                WriteLiteral("\r\n            <div class=\"card-body\">\r\n                <div class=\"row\">\r\n                    <div class=\"col-md-8\">\r\n                        <input type=\"hidden\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1117, "\"", 1134, 1);
#line 26 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Product\Edit.cshtml"
WriteAttributeValue("", 1125, Model.Id, 1125, 9, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1135, 256, true);
                WriteLiteral(@" name=""Id"" />
                        <div class=""form-group row"">
                            <label class=""control-label"" style=""font-weight:bold"">?????? ??????????</label>
                            <input type=""text"" name=""ProductName"" class=""form-control""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1391, "\"", 1417, 1);
#line 29 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Product\Edit.cshtml"
WriteAttributeValue("", 1399, Model.ProductName, 1399, 18, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1418, 270, true);
                WriteLiteral(@" />
                        </div>
                        <div class=""form-group row"">
                            <label class=""control-label"" style=""font-weight:bold"">??????????</label>
                            <input type=""number"" name=""Price"" class=""form-control""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1688, "\"", 1708, 1);
#line 33 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Product\Edit.cshtml"
WriteAttributeValue("", 1696, Model.Price, 1696, 12, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1709, 301, true);
                WriteLiteral(@" />
                        </div>
                        <div class=""form-group row"">
                            <label class=""control-label"" style=""font-weight:bold"">?????? ???????????? ?????????????? ???????????? ????????????</label>
                            <input type=""number"" name=""DaysCount"" class=""form-control""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2010, "\"", 2034, 1);
#line 37 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Product\Edit.cshtml"
WriteAttributeValue("", 2018, Model.DaysCount, 2018, 16, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2035, 216, true);
                WriteLiteral(" />\r\n                        </div>\r\n                        <div class=\"form-group row\">\r\n                            <label class=\"control-label\" style=\"font-weight:bold\">??????????</label>\r\n                            ");
                EndContext();
                BeginContext(2251, 170, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d918afd06991f45f575cc524660c634984e0e64411406", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Name = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
#line 42 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Product\Edit.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = (new SelectList(@ViewBag.cats,"Id", "CatName"));

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2421, 360, true);
                WriteLiteral(@"
                        </div>
                        <div class=""form-group row"">
                            <input type=""submit"" value=""?????? ??????????????????"" class=""btn btn-success btn-block"" id=""btnSave"" />
                        </div>

                    </div>
                    <div class=""col-md-4"">
                        <input type=""hidden""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2781, "\"", 2802, 1);
#line 50 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Product\Edit.cshtml"
WriteAttributeValue("", 2789, Model.ImgUrl, 2789, 13, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2803, 171, true);
                WriteLiteral(" name=\"ImgUrl\" />\r\n                        <input type=\"file\" name=\"imgUploader\" id=\"imgUploader\" class=\"form-control\" style=\"margin-top:25px\" />\r\n                        ");
                EndContext();
                BeginContext(2974, 123, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d918afd06991f45f575cc524660c634984e0e64414439", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 2997, "~/productImgs/", 2997, 14, true);
#line 52 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Product\Edit.cshtml"
AddHtmlAttributeValue("", 3011, Model.ImgUrl, 3011, 13, false);

#line default
#line hidden
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3097, 82, true);
                WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3186, 22, true);
            WriteLiteral("\r\n    </div>\r\n</div>\r\n");
            EndContext();
            DefineSection("scripts", async() => {
                BeginContext(3225, 46, true);
                WriteLiteral("\r\n    <script>\r\n        $(\"#CategoryId\").val(\"");
                EndContext();
                BeginContext(3272, 16, false);
#line 61 "I:\ionic Work\ControlPanel\old versions\wow stable version\WoW Copy\WowApplication\Chat\Fitness\Views\Product\Edit.cshtml"
                         Write(Model.CategoryId);

#line default
#line hidden
                EndContext();
                BeginContext(3288, 1929, true);
                WriteLiteral(@""");
        $(document).ready(function () {
            $(""#imgUploader"").change(function () {
                var img = $(""#imgUploader"").get(0).files[0];
                if (img != """") {
                    $(""#btnSave"").css(""display"", ""block"");
                    //
                    var fileName = img.name;
                    var _index = fileName.lastIndexOf('.') + 1
                    var extensionImg = fileName.substring(_index);
                    var ValidExtension = ['JPG', 'jpg', 'jpeg', 'png', 'PNG', 'GIF', 'gif', 'WEBP', 'TIFF', 'PSD', 'RAW', 'BMP', 'HEIF', 'INDD', 'jpeg 2000', 'JPEG 2000'];
                    if ($.inArray(extensionImg, ValidExtension) == -1) {
                        alert('?????????? ?????????????? ?????? ???? ???????? ????????');
                        $(""#imgUploader"").val('')
                        $(""#imgShow"").css(""display"", ""none"");
                        return false;
                    }
                    //>>>>>>Size
                    var fileSize = img.size /");
                WriteLiteral(@" 1024 / 1024;
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
                            $(""#imgShow"").attr('src', _file.target.result);
                            $(""#imgShow"").css(""display"", ""block"");
                        }
                    }
                }
            })
        })
    </script>
");
                EndContext();
            }
            );
            BeginContext(5220, 6, true);
            WriteLiteral("\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Product> Html { get; private set; }
    }
}
#pragma warning restore 1591
