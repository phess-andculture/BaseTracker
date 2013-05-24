// Common functions
function pad(number, length) {
	var str = '' + number;
	while (str.length < length) { str = '0' + str; }
	return str;
}
function formatTime(time) {
	var hours = parseInt(time / 3600000),
		min = parseInt(time / 6000) - (hours * 60),
		sec = parseInt(time / 100) - (min * 60),
		hundredths = pad(time - (sec * 100) - (min * 6000), 2);
	return (hours > 0 ? pad(hours, 2) : "00") + ":" + (min > 0 ? pad(min, 2) : "00") + ":" + pad(sec, 2);
}
function StartCookie(id) {
	$.cookie("TimerStart", new Date());
	$.cookie("ActiveItemID", id);
}
function EndCookie() {
	$.cookie("TimerStart", null);
	$.cookie("ActiveItemID", null);
}

/**
 * jQuery Timer doesn't natively act like a stopwatch, it only
 * aids in building one.  You need to keep track of the current
 * time in a variable and increment it manually.  Then on each
 * incrementation, update the page.
 *
 * The increment time for jQuery Timer is in milliseconds. So an
 * input time of 1000 would equal 1 time per second.  In this
 * example we use an increment time of 70 which is roughly 14
 * times per second.  You can adjust your timer if you wish.
 *
 * The update function converts the current time to minutes,
 * seconds and hundredths of a second.  It then outputs that to
 * the stopwatch element, $stopwatch, and then increments. Since
 * the current time is stored in hundredths of a second so the
 * increment time must be divided by ten.
 */