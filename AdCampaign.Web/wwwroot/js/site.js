// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const DateTime = luxon.DateTime;
const Interval = luxon.Interval;
const Duration = luxon.Duration;

function GetAdvertTotalTime(fromDate, toDate, fromTime, toTime, includeTime) {
    if (!fromDate || !toDate)
        return null;

    let now = DateTime.local();
    let from = DateTime.fromISO(fromDate);
    from = from > now ? from : now;
    let to = DateTime.fromISO(toDate).plus(Duration.fromObject({days: 1})); // добавляем день к дате окончания
    let dateInterval = Interval.fromDateTimes(from, to);
    if (!dateInterval.isValid)
        return null;

    if (!(fromTime && toDate) || !includeTime) { // без времени просто считаем кол-во часов в промежутке двух дат
        return Math.trunc(dateInterval.length("hours"));
    }

    if (fromTime && toDate) {
        let timeFrom = DateTime.fromISO(fromTime);
        let timeTo = DateTime.fromISO(toTime);
        let timeInterval = Interval.fromDateTimes(timeFrom, timeTo);
        if (!timeInterval.isValid)
            return null;

        let todayHours = 0;
        if (dateInterval.engulfs(timeInterval)) { // если временной интревал сегодняшний полностью входит в интервал дат
            todayHours = Math.trunc(timeInterval.length("hours")); // берем часы временного интервала
        } else if (dateInterval.contains(timeInterval.end)) { // если только конец временного интервала входит ( то есть сегодня время уже больше чем время начала показа)
            let nowWithEndTime = Interval.fromDateTimes(now, timeInterval.end); // берем интервал с сейчас по конец временного
            todayHours = Math.trunc(nowWithEndTime.length("hours"));
        }

        return Math.trunc(dateInterval.length("days")) * Math.trunc(timeInterval.length("hours")) + todayHours;
    }

    return null;
}