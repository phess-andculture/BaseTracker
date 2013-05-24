<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BaseTracker.Web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.0/jquery.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery.timer.js" type="text/javascript"></script>
    <script src="/scripts/timer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span id="stopwatch">00:00:00</span>
			<p>
                <input type='button' value='Play/Pause' onclick='jTimer.Timer.toggle();' />
			    <input type='button' value='Stop/Reset' onclick='jTimer.resetStopwatch();' />
            </p>
    </div>
    </form>
</body>
</html>
