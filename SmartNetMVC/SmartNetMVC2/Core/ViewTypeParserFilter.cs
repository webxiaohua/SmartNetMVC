using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Collections;
using System.CodeDom;
using System.ComponentModel;

namespace Smart.NetMVC2
{
    /// <summary>
    /// 解决Page 继承 泛型类问题
    /// </summary>
    internal class ViewTypeParserFilter : PageParserFilter
    {
        private string _viewBaseType;
        private DirectiveType _directiveType = DirectiveType.Unknown;
        private bool _viewTypeControlAdded;

        /// <summary>
        /// 预处理：页面上的指令
        /// </summary>
        public override void PreprocessDirective(string directiveName, IDictionary attributes)
        {
            base.PreprocessDirective(directiveName, attributes);

            string defaultBaseType = null;

            // If we recognize the directive, keep track of what it was. If we don't recognize
            // the directive then just stop.
            switch (directiveName)
            {
                case "page":
                    _directiveType = DirectiveType.Page;
                    defaultBaseType = typeof(BasePage).FullName;
                    break;
                case "master":
                    _directiveType = DirectiveType.Master;
                    defaultBaseType = typeof(BaseMasterPage).FullName;
                    break;
            }

            if (_directiveType == DirectiveType.Unknown)
            {
                // If we're processing an unknown directive (e.g. a register directive), stop processing
                return;
            }

            // Look for an inherit attribute
            string inherits = (string)attributes["inherits"];
            if (!String.IsNullOrEmpty(inherits))
            {
                // If it doesn't look like a generic type, don't do anything special,
                // and let the parser do its normal processing
                if (IsGenericTypeString(inherits))
                {
                    // Remove the inherits attribute so the parser doesn't blow up
                    attributes["inherits"] = defaultBaseType;

                    // Remember the full type string so we can later give it to the ControlBuilder
                    _viewBaseType = inherits;
                }
            }
        }
        /// <summary>
        /// 是否为泛型类
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private static bool IsGenericTypeString(string typeName)
        {
            // Detect C# and VB generic syntax
            // REVIEW: what about other languages?
            return typeName.IndexOfAny(new char[] { '<', '(' }) >= 0;
        }
        /// <summary>
        ///  解析完成
        /// </summary>
        /// <param name="rootBuilder"></param>
        public override void ParseComplete(ControlBuilder rootBuilder)
        {
            base.ParseComplete(rootBuilder);

            // If it's our page ControlBuilder, give it the base type string
            ViewPageControlBuilder pageBuilder = rootBuilder as ViewPageControlBuilder;
            if (pageBuilder != null)
            {
                pageBuilder.PageBaseType = _viewBaseType;
            }
        }
        /// <summary>
        /// 构建代码
        /// </summary>
        /// <param name="codeType"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public override bool ProcessCodeConstruct(CodeConstructType codeType, string code)
        {
            if (!_viewTypeControlAdded &&
                _viewBaseType != null &&
                _directiveType == DirectiveType.Master)
            {

                // If we're dealing with a master page that needs to have its base type set, do it here.
                // It's done by adding the ViewType control, which has a builder that sets the base type.

                // The code currently assumes that the file in question contains a code snippet, since
                // that's the item we key off of in order to know when to add the ViewType control.

                Hashtable attribs = new Hashtable();
                attribs["typename"] = _viewBaseType;
                AddControl(typeof(ViewType), attribs);
                _viewTypeControlAdded = true;
            }

            return base.ProcessCodeConstruct(codeType, code);
        }

        // Everything else in this class is unrelated to our 'inherits' handling.
        // Since PageParserFilter blocks everything by default, we need to unblock it

        public override bool AllowCode
        {
            get
            {
                return true;
            }
        }

        public override bool AllowBaseType(Type baseType)
        {
            return true;
        }

        public override bool AllowControl(Type controlType, ControlBuilder builder)
        {
            return true;
        }

        public override bool AllowVirtualReference(string referenceVirtualPath, VirtualReferenceType referenceType)
        {
            return true;
        }

        public override bool AllowServerSideInclude(string includeVirtualPath)
        {
            return true;
        }

        public override int NumberOfControlsAllowed
        {
            get
            {
                return -1;
            }
        }

        public override int NumberOfDirectDependenciesAllowed
        {
            get
            {
                return -1;
            }
        }

        public override int TotalNumberOfDependenciesAllowed
        {
            get
            {
                return -1;
            }
        }

        private enum DirectiveType
        {
            Unknown,
            Page,
            UserControl,
            Master,
        }
    }

    internal sealed class ViewPageControlBuilder : FileLevelPageControlBuilder
    {
        public string PageBaseType
        {
            get;
            set;
        }

        public override void ProcessGeneratedCode(
            CodeCompileUnit codeCompileUnit,
            CodeTypeDeclaration baseType,
            CodeTypeDeclaration derivedType,
            CodeMemberMethod buildMethod,
            CodeMemberMethod dataBindingMethod)
        {

            // 如果分析器找到一个有效的类型，就使用它。
            if (PageBaseType != null)
            {
                derivedType.BaseTypes[0] = new CodeTypeReference(PageBaseType);
            }
        }
    }

    /// <summary>
    /// ViewType
    /// </summary>
    [ControlBuilder(typeof(ViewTypeControlBuilder))]
    [NonVisualControl]
    public class ViewType : Control
    {
        private string _typeName;

        /// <summary>
        /// TypeName
        /// </summary>
        [DefaultValue("")]
        public string TypeName
        {
            get
            {
                return _typeName ?? String.Empty;
            }
            set
            {
                _typeName = value;
            }
        }
    }

    internal sealed class ViewTypeControlBuilder : ControlBuilder
    {
        private string _typeName;

        public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, IDictionary attribs)
        {
            base.Init(parser, parentBuilder, type, tagName, id, attribs);

            _typeName = (string)attribs["typename"];
        }

        public override void ProcessGeneratedCode(
            CodeCompileUnit codeCompileUnit,
            CodeTypeDeclaration baseType,
            CodeTypeDeclaration derivedType,
            CodeMemberMethod buildMethod,
            CodeMemberMethod dataBindingMethod)
        {

            // Override the view's base type with the explicit base type
            derivedType.BaseTypes[0] = new CodeTypeReference(_typeName);
        }
    }

}
