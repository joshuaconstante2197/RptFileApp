$(".addCommentButton").click(function () {
    var index = $(".addCommentButton").index(this);
    $(".commentForm:eq(" + index + ")").css("display", "block");
    $(".seeLastCommentButton:eq(" + index + ")").css("display", "block");
    $(".lastComment:eq(" + index + ")").css("display", "none");
    $(".addCommentButton:eq(" + index + ")").css("display", "none");
})

$(".seeLastCommentButton").click(function () {
    var index = $(".seeLastCommentButton").index(this);
    $(".commentForm:eq(" + index + ")").css("display", "none");
    $(".seeLastCommentButton:eq(" + index + ")").css("display", "none");
    $(".lastComment:eq(" + index + ")").css("display", "block");
    $(".addCommentButton:eq(" + index + ")").css("display", "block");
})

