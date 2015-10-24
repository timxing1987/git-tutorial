var urlstr = window.location.href;
urlstr = urlstr.substring(0, urlstr.indexOf('/', 9));

/*获取url中的参数*/
function QueryString() {
    var name, value, i;
    var str = location.href;
    var num = str.indexOf("?")
    str = str.substr(num + 1);
    var arrtmp = str.split("&");
    for (i = 0; i < arrtmp.length; i++) {
        num = arrtmp[i].indexOf("=");
        if (num > 0) {
            name = arrtmp[i].substring(0, num);
            value = arrtmp[i].substr(num + 1);
            this[name] = value;
        }
    }
}

/*
* String.sub().字符串截取(汉字占两位)
*/
String.prototype.sub = function (n) {
    var str = '';
    var r = /[^\x00-\xff]/g;

    if (this == null || this.length <= 0 || this == undefined) {
        return '';
    }
    else if (this.replace(r, "mm").length <= n) {
        return this;
    }
    else {
        var m = Math.floor(n / 2);
        for (var i = m; i < this.length; i++) {
            if (this.substr(0, i).replace(r, "mm").length >= n) {
                return "<abbr title=\"" + this + "\">" + this.substr(0, i) + "…</abbr>";
            }
        }
        return this;
    }
}

//时间格式化
var Dateformat = function (obj, fmt) {
    if (obj != undefined && obj != null && obj != '') {
        if (typeof obj == 'string')
            var dt1 = new Date(Date.parse(obj.replace(/-/g, "/").replace(/T/g," ")));
        else
            var dt1 = obj;
        var o = {
            "M+": dt1.getMonth() + 1, //月份 
            "d+": dt1.getDate(), //日 
            "H+": dt1.getHours(),
            "h+": dt1.getHours(), //小时 
            "m+": dt1.getMinutes(), //分 
            "s+": dt1.getSeconds(), //秒 
            "q+": Math.floor((dt1.getMonth() + 3) / 3), //季度 
            "S": dt1.getMilliseconds() //毫秒 
        };
        if (fmt == undefined || fmt == null || fmt == '')
            fmt = "yyyy-MM-dd";
        if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (dt1.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        return fmt;
    } else {
        return '';
    }
}

var setCookie = function() {
    var expireTime = new Date().getTime() + 1000 * 36000;
    var da = new Date();
    da.setTime(expireTime);
    document.cookie = 'userid=adminid;expires=' + da.toGMTString() + ';path=/';
    document.cookie = 'sessionid=xxxxxxxxxxxxxxxxx;expires=' + da.toGMTString() + ';path=/';
}

/*
* 文件上传(返回文件标识，文件名为空时上传不成功)
* @id              {string} 上传控件id
* @callback        {obj} 回调方法
* @exts            {Exts} 格式类型"gif,jpg,png"
* @fileSize        {number} 文件大小(KB)
* @async          -{bool} 是否异步执行
* return           -1(FileTypeError):文件格式不正确；-2(UploadError):上传异常；-3(FileSizeError)：文件大小超出
*/
var uploadfile = function (id, fileSize, exts, callback, async) {

    var imgTypes = new Array("gif", "jpg", "jpeg", "png", "bmp");
    if (exts) {
        imgTypes = exts.split(",");
    }

    var files = $("#" + id).get(0).files;
    if (files.length <= 0) {
        callback("0");
        return;
    }

    //执行同步还是异步 默认值为false
    async = async || false;
    //文件大小 默认值为2*1024KB
    fileSize = fileSize || 2 * 1024;

    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        if (fileSize != undefined && fileSize !== "") {
            var size = (files[i].size / 1024).toFixed(2);
            if (size > fileSize) {
                if (callback != undefined) {
                    callback("-3");
                }
                return;
            }
        }

        var fileName = files[i].name;
        var ext = fileName.slice(fileName.lastIndexOf(".") + 1).toLowerCase();
        if ($.inArray(ext, imgTypes) < 0) {
            if (callback != undefined) {
                callback("-1");
            }
            return;
        }

        data.append("file" + i, files[i]);
    }

    $.ajax({
        type: "POST",
        url: "/api/Base/FileUpload",
        contentType: false,
        processData: false,
        async: false,
        data: data,
        success: function (results) {
            if (callback != undefined) {
                callback(results);
            }
            return;
        },
        error: function (result) {
            if (callback != undefined) {
                callback("-2");
            }
            return;
        }
    });
}