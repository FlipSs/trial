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
    var req = new XMLHttpRequest();
    req.open("GET", location.protocol + "//" + location.host + "/Blog/Index/?curId=" + encodeURIComponent(postId.replace("post№", "")), true);
    req.send();

    req.onreadystatechange = function () {
        if (req.readyState === 4) {
            if (interval !== null)
                clearInterval(interval);
            if (timer !== null)
                clearTimeout(timer);

                if (req.status === 200) {
                    if(req.responseText === "")
                        answerCont.innerHTML = "<strong>Sorry, but there's no more posts.</strong>";
                    else
                        document.getElementById("article-cont").innerHTML += req.responseText;
                }
                else {
                    answerCont.innerHTML = "<strong>" + req.statusText + "</strong>";
                }

                timer = setTimeout(function () {
                    answerCont.style.display = "none";
                    answerCont.innerHTML = "";
                }, 1000);
        }
    };

    return answerCont;
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