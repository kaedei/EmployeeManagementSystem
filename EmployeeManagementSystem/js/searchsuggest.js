

var searchReq = createReq();
//Create XMLHttpRequest

function createReq() {
	var httpReq;

	if (window.XMLHttpRequest) {
		httpReq = new XMLHttpRequest();
		if (httpReq.overrideMimeType) {
			httpReq.overrideMimeType('text/xml');
		}
	}
	else if (window.ActiveXObject) {
		try {
			httpReq = new ActiveXObject('Msxml2.XMLHTTP');
		}
		catch (e) {
			try {
				httpReq = new ActiveXObject('Microsoft.XMLHTTP');
			}
			catch (e) {
			}
		}
	}
	return httpReq;
}

var textBox;
var tipDiv;
//Send Http request
function searchSuggest(box, tip, para) {
	textBox = box;
	tipDiv = tip;
	var str = escape(document.getElementById(box).value);
	searchReq.open("get", "Suggest.aspx?para=" + para + "&text=" + str, true);
	searchReq.onreadystatechange = handleSearchSuggest;
	searchReq.send(null);
}

//onreadystatechange
function handleSearchSuggest() {
	if (searchReq.readyState == 4) {
		var suggestText = document.getElementById(tipDiv);
		var sourceText = searchReq.responseText.split("\n");
		if (sourceText.length > 0) {
			suggestText.style.display = "";
			suggestText.innerHTML = "";
			for (var i = 0; i < sourceText.length - 1; i++) {
				var s = '<div onmouseover="javascript:suggestOver(this);"';
				s += ' onmouseout="javascript:suggestOut(this);" ';
				s += ' onclick="javascript:setSearch(this.innerHTML);" ';
				s += ' class="suggest_link">' + sourceText[i] + '</div>';
				suggestText.innerHTML += s;
			}
		}
		else {
			suggestText.style.display = "none";
		}

	}
}

function suggestOver(div_value) {
	div_value.className = "suggest_link_over";
}

function suggestOut(div_value) {
	div_value.className = "suggest_link";
}

function setSearch(obj) {
	document.getElementById(textBox).value = obj;
	var div = document.getElementById(tipDiv);
	div.innerHTML = "";
	div.style.display = "none";
}

function tbblur() {
	var div = document.getElementById(tipDiv);
	//div.innerHTML = "";
	div.style.display = "none";
}

