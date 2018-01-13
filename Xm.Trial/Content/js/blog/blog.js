'use strict';

window.onload = function () {
    var divCont = document.getElementById("move-cont");
    var timer;
    window.onscroll = function () {
        if (Math.round(window.scrollY) >= document.body.scrollHeight - window.innerHeight) {
            divCont = getPosts(move(divCont), timer);
        }
    };
};

function getPosts(params, timer)
{
    var answerCont = params[0];
    var interval = params[1];
    var postId = document.getElementById("article-cont").lastElementChild.id;

    var promise = sendAsyncGetRequest(location.protocol + "//" + location.host + "/Blog/Index/?curId=" + encodeURIComponent(postId.replace("post_", "")));

    if (interval !== null)
        clearInterval(interval);
    if (timer !== null)
        clearTimeout(timer);
    
    promise.then(function (response) {
        if (response === "")
            answerCont.innerHTML = "<strong>Sorry, but there's no more posts.</strong>";
        else
            document.getElementById("article-cont").innerHTML += response;
    }, function (reason) {
        answerCont.innerHTML = "<strong>" + reason + "</strong>";
    }
    );

    timer = setTimeout(function () {
        answerCont.style.display = "none";
        answerCont.innerHTML = "";
    }, 1000);

    return answerCont;
}

function sendAsyncGetRequest(url)
{
    return new Promise((resolve, reject) => {
        const req = new XMLHttpRequest();
        req.open("GET", url, true);
        req.setRequestHeader("X-Requested-With", "XMLHttpRequest");
        req.onload = () => resolve(req.responseText);
        req.onerror = () => reject(req.statusText);
        req.send();
    });
}

function move(divCont)
{
    divCont.style.display = "inline-block";
    var div = document.createElement("div");
    div.className = "moved";
    var interval = setInterval(function () {
            if (divCont.children.length === 5) {
                clearInterval(interval);
            }
            else {
                divCont.appendChild(div.cloneNode());
            }
        }, 150);

    return [divCont, interval];
}