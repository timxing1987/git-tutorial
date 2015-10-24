// jquery common extend
$.fn.extend({
    menu_tab: function () {
        var $ul = $(this);
        $ul.find('li>a').click(function () {
            // removeClass
            var id = $ul.find('li[class="active"]>a').attr("data-tab-target");
            $(id + '[class*="active"]').removeClass("active");
            $ul.find('li[class="active"]').removeClass("active");
            // addClass
            $(this).parent().addClass("active");
            $($(this).attr("data-tab-target")).addClass("active");
        });
    },
    ul: function () {
        var base = $(this);
        $(base).find('li').click(function () {
            $(base).find('li.active').removeClass("active");
            $(this).addClass("active");
        });
    }
});


/* 
更多按钮扩展方法 
参数：jsonArr,json数组,
     单个json对象格式{'icon':'icon-edit','text':'删除职员','property':'自定义属性（用于获取当前点击的对象）'}
 @author Jake.Wang
 @date 2014-11-12
*/
$.fn.extend({
    tooltip_btnlist: function (jsonArr) {
        // 当前对象
        var obj = this;
        var title = '<ul class="btn-list">';
        $.each(jsonArr, function (key, json) {
            title += '<li><a href="javascript:void(0);" ' + json.property + '><i class="' + json.icon + '"></i>' + json.text + '</a></li>';
        });

        title += '</ul>';

        // tooltip配置
        $(obj).tooltip({
            placement: 'left',
            html: true,
            title: title,
            trigger: 'focus',
            template: '<div class="tooltip" role="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-inner"></div></div>'
        });
    }
});

// 分页控件
$.fn.extend({
    /**
    * @author Yam.Ren
    * @date   2014-03-18
    * @description  jquery扩展方法
    * 参数说明：
    * @pageSize {int}       每页显示数量，如果不需要分页，此参数传0即可
    * @webApi   {string}    请求的WebApi
    * @query    {json}      查询条件
    * @callBack {function}  回调函数
    * @isbigdata {bool}  是否是大数据,默认为true,
    * @return   {object}    DataTables enhanced之后的对象
    */
    getPageListWithSort: function (pageSize, webApi, query, callBack, isbigdata) {

        var sEmptyTable = "抱歉！暂无数据...";
        var skip = '跳转';
        if (isbigdata == undefined || isbigdata == false) {
            query.PagingQueryType = 3;
        }
        else {
            
        }

        var pagingObj = $(this);

        var iRequestStart = 0, iRequestLength = 0, iEcho = 0;

        var pagingid = 'pagination_' + this[0].id;
        var pgobj = $("#" + pagingid);
        if (pgobj.length > 0) {
            pgobj.remove();
        }

        $(this).after('<div id="' + pagingid + '"></div>');
        pgobj = $("#" + pagingid);

        var pageindex = 1;
        //设置是否分页
        if (pageSize <= 0) {
            query["Paging"] = false;
        }
        else {
            query["Paging"] = true;
        }
        query["PageIndex"] = pageindex;
        query["PageSize"] = pageSize;

        //计算总页数
        var GetTotalPages = function (maxcount, pagesize) {
            var maxindex = 0;
            if (maxcount <= 0) {
                maxindex = 1;
            }
            else {
                var ys = maxcount % pagesize;
                if (ys == 0) {//整除
                    maxindex = maxcount / pagesize;
                }
                else {
                    maxindex = (maxcount - ys) / pagesize + 1;
                }
            }
            return maxindex;
        };
        //请求数据
        var GetData = function (json, isgetcount) {
            var startindex = (query["PageIndex"] - 1) * pageSize + 1;
            //不需要大数据处理
            if (json.PagingQueryType != undefined && json.PagingQueryType == 3) {
                json.PagingQueryType = 1;//只返回列表数据

                $.post(webApi, json, function (result) {
                    if (result == null) {
                        callBack(null, startindex);
                        return;
                    }
                    callBack(result.aaData, startindex);
                    var maxcount = result["iTotalRecords"];
                    showpaging(maxcount);
                });
            }
            else {
                json["PagingQueryType"] = 1;//只返回列表数据
                $.post(webApi, json, function (result) {
                    if (result == null) {
                        callBack(null, startindex);
                        return;
                    }
                    callBack(result.aaData, startindex);
                });

                if (isgetcount) {
                    json["PagingQueryType"] = 2;//只计算总数目
                    $.post(webApi, json, function (result) {
                        if (result == null) {
                            return;
                        }
                        var maxcount = result["iTotalRecords"];
                        showpaging(maxcount);
                    });
                }
            }
        };
        //分页条
        var showpaging = function (maxcount) {
            var totalPages = GetTotalPages(maxcount, pageSize);
            if (maxcount <= 0) {              
                var colnum = pagingObj.find("tr").eq(0).find("th").length;
                var trstr = '<tr><td colspan="' + colnum + '">' + sEmptyTable + '</td></tr>';
                if (pagingObj.find("tfoot").length > 0) {
                    pagingObj.find("tfoot").html(trstr);
                }
                else {
                    pagingObj.append("<tfoot>" + trstr + "</tfoot>");
                }
            }
            else {
                pagingObj.find("tfoot").remove();
                $("#" + pagingid).twbsPagination({
                    totalPages: totalPages,
                    visiblePages: 5,
                    first: null,
                    prev: '上一页',
                    next: '下一页',
                    skip: skip,
                    goPage: 0,
                    last: null,
                    pagenum: '<span><input type="text" value="1" /></span>',
                    onPageClick: function (event, page) {
                        $("#" + pagingid + " .pagination").find("input").val(page);
                        query["PageIndex"] = page;
                        GetData(query, false);
                        // 回调函数，加载数据
                    }
                });
            }
        };
        GetData(query, true);
        return null;
    }
});

/********************模拟下拉框扩展方法chosen*****start******************/

$.fn.extend({
    chosenEmpty: function () {//chosen控件清空
        this.empty().append("<option></option>").trigger("liszt:updated");
    },
    changeVal: function (val) {//chosen模拟框改变值
        this.val(val).trigger("liszt:updated");
    },
    disabled: function () {//chosen模拟框禁用
        this.attr('disabled', 'disabled').trigger("liszt:updated");
    },
    enabled: function () {//chosen模拟框启用
        this.removeAttr('disabled').trigger("liszt:updated");
    },
    appendObj: function (value, text) {//chosen添加option
        this.append("<option value='" + value + "' title='" + text + "'>" + text + "</option>").trigger("liszt:updated");
    },
    //绑定数据jsonData：数据，valueName：value元素名，textName：text元素名
    chosenBindData: function (jsonData, valueName, textName) {
        var $obj = this;
        $obj.empty();
        $obj.append("<option><option>");
        //重载方法1:textName为string类型
        if (typeof (textName) == "string") {
            $.each(jsonData, function (index, value) {
                var valval = "";
                var textval = "";
                if (valueName != undefined) {
                    valval = value[valueName];
                }
                else {
                    valval = value;
                }
                if (textName != undefined) {
                    textval = value[textName];
                }
                else {
                    textval = value;
                }
                var opt = $('<option />', {
                    value: valval,
                    text: textval
                });
                opt.appendTo($obj);
            });
        } else {
            //重载方法2:textName为['', '']类型
            $.each(jsonData, function (index, value) {
                var valval = "";
                var textval = "";
                if (valueName) {
                    valval = value[valueName];
                }
                else {
                    valval = value;
                }
                if (textName) {
                    $.each(textName, function (i, txt) {
                        textval += value[txt];
                    });
                }
                else {
                    textval = value;
                }
                var opt = $('<option />', {
                    value: valval,
                    text: textval
                });
                opt.appendTo($obj);
            });
        }
        $obj.trigger("liszt:updated");
    },
    selectFirst: function () {//选中第一个value不为''的值
        var val = '';
        $(this).children().each(function () {
            if ($(this).val() != undefined && $(this).val() != null && $(this).val() != '') {
                val = $(this).val();
                return false;
            }
        })
        if (val != '') this.val(val).trigger("liszt:updated");
    },
    txt: function () {
        return $(this).find('option[value=' + $(this).val() + ']').text();
    },
    hideObj: function () {
        $(this).next().hide();
    },
    showObj: function () {
        $(this).next().show();
    }
});
/****************end**********************************/

/************多选模拟下拉框扩展方法*******************/
$.fn.extend({
    mutlidisabled: function () {
        this.multiselect('disable');
    },
    mutlienabled: function () {
        this.multiselect('enable');
    },
    mutliCheckAll: function ()//全选
    {
        this.multiselect('checkAll');
    },
    mutliUncheckAll: function () {//取消全选
        this.multiselect('uncheckAll');
    },
    mutliChangeVal: function (val) {//设置选中值以逗号分值
        if (val) {
            var strs = val.split(',');
            for (var i = 0; i < strs.length; i++) {
                $(this).find("option[value='" + strs[i] + "']").attr("selected", "true")
            }
            this.multiselect('refresh');
        }
    },
    /**
    *添加一个选项
    * @val   {string}  value
    * @txt   {string}  text
    * @state {string}  1:selected 2:disabled
    */
    mutliAppendObj: function (val, txt, state) {
        var opt = $('<option />', {
            value: val,
            text: txt
        });
        if (state == '2') {
            opt.attr('disabled', 'disabled');
        }
        if (state == '1') {
            opt.attr('selected', 'selected');
        }
        opt.appendTo(this);
        this.multiselect('refresh');
    },
    mutlitxt: function ()//获取选中的txt值
    {
        var str = this.multiselect('txt');
        return str;
    },
    //绑定数据jsonData：数据，valueName：value元素名，TextName：text元素名
    mutliBindData: function (jsonData, valueName, TextName) {
        var $obj = this;
        $obj.empty();
        $.each(jsonData, function (index, value) {
            var opt = $('<option />', {
                value: value[valueName],
                text: value[TextName]
            });
            //if (state == '2') {
            //    opt.attr('disabled', 'disabled');
            //}
            //if (state == '1') {
            //    opt.attr('selected', 'selected');
            //}
            opt.appendTo($obj);

        });
        $obj.multiselect('refresh');
    },
    refresh: function () {
        this.multiselect('refresh');
    }
});
/****************end*******************************/

//#region 图片上传控件封装
/*
 * imageUpload json参数
 * defultimg：初始化图片
 * maxsize：允许最大大小 单位byte
 * fileExt：支持文件类型，默认为 "*.bmp; *.png; *.jpeg; *.jpg; *.gif"，格式如默认，分号隔开
 * showborder：是否显示外框，默认true显示
 * onImgLoaded：当图片加载后触发事件
 * bindUpload参数：可以是一个guid，也可以是一个图片地址连接，也可以是/showimg/+图片的guid,图片的guid会保存在input[type=file]的"data-id"属性中
 */
$.fn.extend({
    imageUpload: function (json) {
        var defultsrc = "";
        var imagesrc = "";
        var fileExt = "*.bmp; *.png; *.jpeg; *.jpg; *.gif";
        var filesize = 1024 * 200;//200kb
        var onimgloaded = function (src) {

        };
        if (json.defultimg != undefined && json.defultimg != null) {
            defultsrc = json.defultimg;
        }
        if (json.maxsize != undefined && json.maxsize != null) {
            filesize = json.maxsize;
        }
        if (json.fileExt != undefined && json.fileExt != null) {
            fileExt = json.fileExt;
        }
        if (json.onImgLoaded != undefined && json.onImgLoaded != null) {
            onimgloaded = json.onImgLoaded;
        }

        var borderhtml = "border: 1px solid rgb(204, 204, 204);";
        if (json.showborder === false) {
            borderhtml = "";
        }
        var fileele = $(this)[0];
        var imgheight = $(this).height();
        var imgwidth = $(this).width();
        var opt = $('<div />', {
            "style": $(this).attr("style") + ";position:relative;float:left;cursor: pointer;" + borderhtml,
            "class": $(this).attr("class"),
            "html": '<a href="#" style="display: block;width: 20px;height: 20px;line-height: 20px;text-align: right;position: absolute;top: 2px;right: 10px;display:none;background: url(\'/lib/css/images/a2.png\') no-repeat;"></a>' +
                '<div style="text-align:center;margin:auto;width:' + imgwidth + 'px;height:' + imgheight + 'px;line-height:' + imgheight + 'px;">' +
                  '<img src="' + defultsrc + '" style="display: inline-block;vertical-align: middle;max-height: 100%;max-width: 100%;"/>' +
                '</div>'
        });
        opt.find("img").on("load", function () {
            onimgloaded($(this).attr("src"));
        });
        $(this).after(opt);

        //图片转化为地址
        var getFileUrl = function (fileele) {
            var url;
            if (navigator.userAgent.indexOf("MSIE") >= 1) { // IE 
                url = fileele.value;
            } else if (navigator.userAgent.indexOf("Firefox") > 0) { // Firefox 
                url = window.URL.createObjectURL(fileele.files.item(0));
            } else if (navigator.userAgent.indexOf("Chrome") > 0) { // Chrome 
                url = window.URL.createObjectURL(fileele.files.item(0));
            }
            return url;
        };

        //图片点击
        $(opt).find("div").bind("click", function () {
            fileele.click();
        });

        //鼠标移出
        $(opt).find("a").bind("click", function () {
            $(opt).find("img").attr("src", defultsrc);
            fileele.value = null;
            //图片点击
            $(opt).find("div").bind("click", function () {
                fileele.click();
            });
            $(opt).find("a").hide();
            //鼠标移入
            $(opt).unbind("mouseover");
            //鼠标移出
            $(opt).unbind("mouseout");
        });
        //上传控件内容变化
        $(this).bind("change", function () {
            var filevalue = fileele.value;
            if (filevalue == undefined || filevalue == null || filevalue == "") {
                //$(opt).find("img").attr("src", getFileUrl(fileele));
            }
            else {
                $opt = $(opt);
                if (fileExt.indexOf(fileele.files[0].type.replace("image/", "")) < 0) {
                    alertObj('文件格式不支持！', 'warning');
                    return;
                }
                if (fileele.files[0].size > filesize) {
                    alertObj('当前选择的文件超过限定的大小' + (filesize / 1024) + 'kb，请重新选择文件！', 'warning');
                    return;
                }
                //fileele.files[0]
                $opt.find("img").attr("src", getFileUrl(fileele));

                //绑定图片后
                $opt.find("div").unbind("click");

                //鼠标移入
                $opt.bind("mouseover", function () {
                    $(opt).find("a").show();
                });
                //鼠标移出
                $opt.bind("mouseout", function () {
                    $(opt).find("a").hide();
                });
            }
        });
        $(this).hide();
    }
    , bindUpload: function (imgid) {
        var imgsrc = "";
        var id = "";
        //针对SR封装
        if (imgid != undefined && imgid != null) {
            if (imgid.toLowerCase().indexOf("/showimg/") >= 0) {
                id = imgid.replace("/showimg/", "");
                imgsrc = imgid;
            }
            else if (imgid.toLowerCase().indexOf("/") >= 0) {
                id = imgid;
                imgsrc = imgid;
            }
            else {
                id = imgid;
                imgsrc = /showimg/ + id;
            }
        }
        $(this).attr("data-id", id);
        var $opt = $(this).next();
        $opt.find("img").attr("src", imgsrc);


        //绑定图片后
        $opt.find("div").unbind("click");

        //鼠标移入
        $opt.bind("mouseover", function () {
            $opt.find("a").show();
        });
        //鼠标移出
        $opt.bind("mouseout", function () {
            $opt.find("a").hide();
        });
    }
});

//#endregion


$.fn.extend({
    /**
    *控件弹出验证信息
    * @name   {string}  多语言name值 
    */
    showMsgError: function (name) {
        if (name == undefined || name == null || name == "") {
            name = "notrequired";
        }
        $(this).validationEngine("showPrompt", getLanguage(name), "error", false, true);
    }
});



$.fn.extend({
    labeltxt: function (value, num) {
        try {
            var newLength = 0;
            var newStr = "";
            var chineseRegex = /[^\x00-\xff]/g;
            var singleChar = "";
            var strLength = value.replace(chineseRegex, "**").length;
            for (var i = 0; i < strLength; i++) {
                singleChar = value.charAt(i).toString();
                if (singleChar.match(chineseRegex) != null) {
                    newLength += 2;
                } else {
                    newLength++;
                }
                if (newLength > num) {
                    break;
                }
                newStr += singleChar;
            }
            if (strLength > num) {
                newStr += "...";
                $(this).attr('title', value)
            }
            $(this).text(newStr);
        }
        catch (e) {
            $(this).text(value);
        }
    },
    //判断文字是否超出长度，中英文
    checkMaxLength: function (lenTemp) {
        var r = /[^\x00-\xff]/g;
        if ($(this).val().replace(r, "mm").length <= lenTemp) return $(this).val();
        var m = Math.floor(lenTemp / 2);
        for (var i = m; i < $(this).val().length; i++) {
            if ($(this).val().substr(0, i).replace(r, "mm").length >= lenTemp) {
                //alert('注意：文字长度不能超过'+lenTemp+',系统自动将超出文字去掉!');
                return $(this).val().substr(0, i);
            }
        }
        return $(this).val();
    },
    //显示错误提示验证
    showValidError: function (option) {
        if (option != undefined && option.type != undefined) {
            switch (option.type) {
                case "required":
                    $(this).validationEngine("showPrompt", getLanguage('mustrequired'), "error", false, true);
                    break;
            }
        } else {
            $(this).validationEngine("showPrompt", getLanguage('mustrequired'), "error", false, true);
        }
    },
    //隐藏错误提示
    hideValidError: function (callback) {
        $(this).validationEngine("hide");
        if (callback != undefined && callback != null && callback != "") {
            callback();
        }
    },
    // 必填项效果
    requireValid: function () {
        if ($(this).is("input")) {
            if (!$(this).hasClass("requireValid")) {
                $(this).addClass("requireValid");
            }
        }
        if ($(this).is("select")) {
            if (!$(this).next("div").find("a").hasClass("requireValid")) {
                $(this).next("div").find("a").addClass("requireValid");
            }
        }
    }
});
