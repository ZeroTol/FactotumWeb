// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.




// JavaScript object for dashboard handle
var fjs = window.fjs || {};

"use strict";
fjs.boardManager = fjs.boardManager || {};

fjs.boardManager.prepareMainDashboard = function () {

    alertMeBaby = function () {
        alert("Hello Baby!");
    };

    iLikeIt = function (element) {
        var article_id = $(element).attr("article-row-id");
        alert(`You like article number: ${article_id}`);
    };

    iDislikeIt = function (element) {
        var article_id = $(element).attr("article-row-id");
        alert(`You dislike article number: ${article_id}`);
    };

    assignEventsToControls = function () {

        //var element = $("#divTest");
        //element.click(function () {
        //    iLikeIt(this);
        //});

        // assign action to all like buttons
        $(".likeAction").click(function () {
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
        $(".dislikeAction").click(function () {
            iDislikeIt(this);
        });

        //Mobile Create & Cancel posting Script
        $("#create-post").click(function (e) {
            e.preventDefault();
            $(".posting").toggleClass("active");
            $(".mb").toggleClass("deactive");
        });

        $("#cancel-post").click(function (e) {
            e.preventDefault();
            $(".posting").toggleClass("active");
            $(".mb").toggleClass("deactive");
        });
    };

    init = function () {
        //alertMeBaby();
        assignEventsToControls();

    }();
}();

