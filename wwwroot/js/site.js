// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.




// JavaScript object for dashboard handle
var fjs = window.fjs || {};

"use strict";
fjs.boardManager = fjs.boardManager || {};

fjs.boardManager.prepareMainDashboard = function () {

    setCookie = function (e) {
        var cname = ".factotumPrivacyAndPolicy";
        var cvalue = "yes";
        var exdays = 10;

        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";

        //TODO
        //debugger;
        //var x = $(document).find(".toast");
        //x[0].style.display = "none";

    };

    getCookie = function (cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) === 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    };

    factotumCookiePrivacyAndPolicy = function () {
        var result = getCookie(".factotumPrivacyAndPolicy");
        debugger;
        return result;

    };

    checkCookie = function (e) {
        var username = getCookie(".username");
        if (username !== "") {
            alert("Welcome again " + username);
        } else {
            username = prompt("Please enter your name:", "");
            if (username !== "" && username !== null) {
                setCookie(".userName", username, 365);
            }
        }
    };


    alertMeBaby = function () {
        alert("Hello Baby!");
    };

    iLikeIt = function (element) {
        //var article_id = $(element).attr("article-row-id");
        //alert(`You like article number: ${article_id}`);

        //$('*[data-vote-plus]').click(function (e) {
        debugger;
        //var requestUrl = `https://localhost:5001/api/message/LikeFact`;
        var requestUrl = `https://factotumapi.azurewebsites.net/api/message/LikeFact`;

        var fact_id = $(element).attr("vote-id");

        $.ajax({
            type: "POST",
            url: requestUrl,
            data: JSON.stringify(fact_id),
            success: function (data) {
                debugger;
                alert("Thanks for your like ;-)");
            },
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        });
        //});

    };

    iDislikeIt = function (element) {
        //var article_id = $(element).attr("article-row-id");
        //alert(`You dislike article number: ${article_id}`);

        //$('*[data-vote-minus]').click(function (e) {
        //var requestUrl = `https://localhost:5001/api/message/LikeFact`;
        var requestUrl = `https://factotumapi.azurewebsites.net/api/message/DislikeFact`;

        var fact_id = $(element).attr("vote-id");

        $.ajax({
            type: "POST",
            url: requestUrl,
            data: JSON.stringify(fact_id),
            success: function (data) {
                debugger;
                alert("Thanks for your dislike ;-)");
            },
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        });
        //});
    };

    assignEventsToControls = function () {

        //$("#testbtnCreate").click(setCookie);
        //$("#testbtnCheck").click(checkCookie);



        $("#factotumPrivacyAndPolicy").click(setCookie);

        $("#by_rating_button").click(getAndRefreshDashBoardData);

        $("#by_rating_button").click(getAndRefreshDashBoardData);

        $("#latest_rated_button").click(getAndRefreshDashBoardData);

        $("#latest_added_button").click(getAndRefreshDashBoardData);

        // assign action to all like buttons
        $('*[data-vote-plus]').click(function () {
            iLikeIt(this);
        });

        //$(".vote_plus").click(function (e) {
        //    var vote_id = $(this).attr("vote-id");

        //    var fact_id = 777;

        //    $.ajax({
        //        type: "POST",
        //        url: "https://localhost:44347/api/FakeApi/AllAuthor",
        //        data: JSON.stringify(fact_id),
        //        success: function (data) {
        //            debugger;
        //        },
        //        dataType: "json",
        //        contentType: "application/json; charset=utf-8"
        //    });
        //});

        // assign action to all dislike buttons
        $('*[data-vote-minus]').click(function () {
            iDislikeIt(this);
        });



        //Mobile Create & Cancel posting Script
        //$("#create-post").click(function (e) {
        //    e.preventDefault();
        //    $(".posting").toggleClass("active");
        //    $(".mb").toggleClass("deactive");
        //});

        //$("#cancel-post").click(function (e) {
        //    e.preventDefault();
        //    $(".posting").toggleClass("active");
        //    $(".mb").toggleClass("deactive");
        //});
    };

    getAndRefreshDashBoardData = function (e) {
        var dataOrder = $(this).attr("data-order-type");
        //var requestUrl = `https://localhost:5001/api/message/dashboard/${dataOrder}`;
        var requestUrl = `https://factotumapi.azurewebsites.net/api/message/dashboard/${dataOrder}`;

        var prepareHashTags = function (hashTags) {
            var temp_str = ``;

            debugger;
            var temp_arr = hashTags.split("#");

            temp_arr.forEach(item => {
                if (item.length > 0) {
                    temp_str = temp_str +
                        `&nbsp;<a href="#" class="badge badge-primary">#${item.trim()}</a>`;
                }
            });
            return temp_str;
        }

        var prepareDisplayDate = function (date) {
            var month_and_day = date.slice(5, 10);
            var day_and_month = month_and_day.split("-")[1] + "-" + month_and_day.split("-")[0];
            return day_and_month;
        }


        $.get(requestUrl, function (data) {
            if ((data) && (data.length > 0)) {
                var content = ``;
                data.forEach(e => {


                    content = content + `
<div class='row table-post'>
            <div class='col-md-7 col-8 order-md-1 order-1'>
                ${(e.factTitle)}
                <br />
                ${((e.factHashTags !== null) && (e.factHashTags.length > 0))
                            ? prepareHashTags(e.factHashTags)
                            : ``}
            </div>
            <div class='col-md-1 col-6 order-md-2 order-4 post-time text-right'>
                ${((e.factCreateDate) && (e.factCreateDate.length >= 10)) ? prepareDisplayDate(e.factCreateDate) : ''.toString()}
            </div>
            <div class='col-md-2 col-6 order-md-3 order-3 post-link'>
                <a href='url'>
                    ${e.factLink}
                </a>
            </div>
            <div class='col-md-2 col-4 order-md-4 order-2 text-right'>
                <div class='row'>
                    <div class='col-lg-4 col-md-12 col-12 order-lg-1 order-md-2 order-2 text-right'>
                        <div class='  btn-group-toggle post_vote' data-toggle='buttons'>
                            <label class='btn btn-rate'>
                                <input type='radio' name='options' id='option1' autocomplete='off'>-
                            </label>
                            <label class='btn btn-rate  '>
                                <input type='radio' name='options' id='option2' autocomplete='off'>+
                            </label>
                        </div>
                    </div>
                    <div class='col-lg-8 col-md-12 col order-lg-2 order-md-1 order-1 text-right'>
                        <div class='post_rating golden'>

                            ${((e.factRating) && (parseInt(e.factRating)) > 0)
                            ? `+` + e.factRating : e.factRating
                        }
                        <div class='golden-dot'></div>
                        </div>
                    </div>
                </div>
            </div>
</div>
`;
                    $("#content_container").html(content);
                })
            }
        });
    }


    init = function () {
        //alertMeBaby();
        assignEventsToControls();

        //$(document).ready(function () {
        //    $('.toast').toast('show');
        //});


        //var x = factotumCookiePrivacyAndPolicy();
        //debugger;

        //if (x !== "yes") {
        //    $(document).ready(function () {
        //        //$('.toast').toast('show');
        //        var x = $(document).find(".toast");
        //        x[0].style.display = "flex";
        //    });
        //}

        $(document).ready(function () {
            $('.toast').toast('show');
        });

    }();
}();

