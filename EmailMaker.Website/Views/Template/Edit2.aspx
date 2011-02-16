<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="Core.Web.Extensions" %>
<%@ Import Namespace="EmailMaker.Controllers.Template" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Edit2</title>
</head>
<body>
    <div>
        <%: Html.UrlFromControllerMethodWithoutParameters<TemplateController>(c => c.CreateVariable(null)) %>
    </div>
</body>
</html>
