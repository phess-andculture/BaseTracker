<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BaseTracker.Web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.0/jquery.min.js" type="text/javascript"></script>
	<script src="/scripts/jquery.timer.js" type="text/javascript"></script>
	<script src="/scripts/jquery.cookie.js" type="text/javascript"></script>
	<script src="/scripts/timer.js" type="text/javascript"></script>
	<script>
		var curTime = 0;
		var autoStart = false;
		var jTimer;
		var smallestMinute = 15

		$(document).ready(function () {
			if ($.cookie("TimerStart") != null) {
				curTime = Math.round((new Date() - new Date($.cookie("TimerStart")))/10);
				autoStart = true;
			}
			$("#stopwatch").html(formatTime(curTime));
			jTimer = new (function () {
				var $stopwatch, // Stopwatch element on the page
					incrementTime = 1000, // Timer speed in milliseconds
					currentTime = curTime;
					updateTimer = function () {
						$stopwatch.html(formatTime(currentTime));
						currentTime += incrementTime / 10;
				},
				init = function () {
					$stopwatch = $('#stopwatch');
					jTimer.Timer = $.timer(updateTimer, incrementTime, autoStart);
				};
				this.stopStopwatch = function () {
					this.Timer.stop().once();
					$("#finalTime").val(formatTimeRounding(currentTime));
				};
				$(init);
			});
		});
		function StartCookieLocal() {
			if ($.cookie("TimerStart") == null) {
				StartCookie($("#taskID").val());
			}
		}
		function formatTimeRounding(time) {
			var hours = parseInt(time / 3600000),
				min = parseInt(time / 6000) - (hours * 60)
			return (hours > 0 ? pad(hours, 2) : "00") + ":" + (min > 0 ? pad(min, 2) : "00");
		}
		function validateEntry() {
			
		}
	</script>
</head>
<body>
	<div runat="server" id="to_do_timer" visible="false">
		<form id="form1" runat="server">
			<div>
				<label for="txtParent">Current Item: </label>
				<asp:Label runat="server" ID="lblParent" /><br />
				<br />
				<span id="stopwatch"></span>
				<p>
					<input type='button' value='Play/Pause' onclick='jTimer.Timer.toggle(); StartCookieLocal();' />
					<input type='button' value='Stop' onclick='jTimer.stopStopwatch();' />
				</p>
				<input type="text" id="finalTime" name="finalTime" />
				<input type="button" value="Submit Time" />
				<input type="submit" value="Submit Time" onclick="return validateEntry();"/>
				<input type="hidden" id="taskID" name="taskID" value="<%= ItemID %>" />
			</div>
		</form>
	</div>
	<div runat="server" id="projects" visible="false">
		<asp:Repeater ID="rptProjects" runat="server">
			<HeaderTemplate>
				<ul class="projectTree">
			</HeaderTemplate>
			<ItemTemplate>
				<li><%# Eval("Company") %> - <%# Eval("Name") %></li>
				<asp:Repeater runat="server" ID="rptProjectTodo" DataSource='<%# Eval("TodoListArray") %>'>
					<HeaderTemplate>
						<ul class="TodoTree">
					</HeaderTemplate>
					<ItemTemplate>
						<li><%# Eval("Name") %></li>
						<asp:Repeater runat="server" ID="rptProjectTodoItem" DataSource='<%# Eval("ListItem") %>'>
							<HeaderTemplate>
								<ul class="ItemTree">
							</HeaderTemplate>
							<ItemTemplate>
								<li><a href="/Default.aspx?q=I<%# Eval("ID") %>"><%# Eval("Name") %></a></li>
							</ItemTemplate>
							<FooterTemplate>
								</ul>
							</FooterTemplate>
						</asp:Repeater>
					</ItemTemplate>
					<FooterTemplate>
						</ul>
					</FooterTemplate>
				</asp:Repeater>
			</ItemTemplate>
			<FooterTemplate>
				</ul>
			</FooterTemplate>
		</asp:Repeater>
	</div>
</body>
</html>
