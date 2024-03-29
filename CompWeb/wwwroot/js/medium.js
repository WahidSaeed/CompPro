﻿function MediumEditor(a, b) {
    "use strict";
    return this.init(a, b)
}
"object" == typeof module && (module.exports = MediumEditor),
    function (a, b) {
        "use strict";

        function c(a, b) {
            var c;
            if (void 0 === a) return b;
            for (c in b) b.hasOwnProperty(c) && a.hasOwnProperty(c) === !1 && (a[c] = b[c]);
            return a
        }

        function d() {
            var b, c, d, e = a.getSelection();
            if (e.getRangeAt && e.rangeCount) {
                for (d = [], b = 0, c = e.rangeCount; c > b; b += 1) d.push(e.getRangeAt(b));
                return d
            }
            return null
        }

        function e(b) {
            var c, d, e = a.getSelection();
            if (b)
                for (e.removeAllRanges(), c = 0, d = b.length; d > c; c += 1) e.addRange(b[c])
        }

        function f() {
            var a = b.getSelection().anchorNode,
                c = a && 3 === a.nodeType ? a.parentNode : a;
            return c
        }

        function g() {
            var c, d, e, f, g = "";
            if (void 0 !== a.getSelection) {
                if (d = a.getSelection(), d.rangeCount) {
                    for (f = b.createElement("div"), c = 0, e = d.rangeCount; e > c; c += 1) f.appendChild(d.getRangeAt(c).cloneContents());
                    g = f.innerHTML
                }
            } else void 0 !== b.selection && "Text" === b.selection.type && (g = b.selection.createRange().htmlText);
            return g
        }

        function h(a) {
            return !(!a || 1 !== a.nodeType)
        }
        MediumEditor.prototype = {
            defaults: {
                allowMultiParagraphSelection: !0,
                anchorInputPlaceholder: "Paste or type a link",
                anchorPreviewHideDelay: 500,
                buttons: ["bold", "italic", "underline", "anchor", "header1", "header2", "quote"],
                buttonLabels: !1,
                checkLinkFormat: !1,
                cleanPastedHTML: !1,
                delay: 0,
                diffLeft: 0,
                diffTop: -10,
                disableReturn: !1,
                disableDoubleReturn: !1,
                disableToolbar: !1,
                disableEditing: !1,
                elementsContainer: !1,
                firstHeader: "h2",
                forcePlainText: !0,
                placeholder: "Type your regulation section here",
                secondHeader: "h3",
                targetBlank: !1,
                extensions: {},
                activeButtonClass: "medium-editor-button-active",
                firstButtonClass: "medium-editor-button-first",
                lastButtonClass: "medium-editor-button-last"
            },
            isIE: "Microsoft Internet Explorer" === navigator.appName || "Netscape" === navigator.appName && null !== new RegExp("Trident/.*rv:([0-9]{1,}[.0-9]{0,})").exec(navigator.userAgent),
            init: function (a, d) {
                return this.setElementSelection(a), 0 !== this.elements.length ? (this.parentElements = ["p", "h1", "h2", "h3", "h4", "h5", "h6", "blockquote", "pre"], this.id = b.querySelectorAll(".medium-editor-toolbar").length + 1, this.options = c(d, this.defaults), this.setup()) : void 0
            },
            setup: function () {
                this.isActive = !0, this.initElements().bindSelect().bindPaste().setPlaceholders().bindWindowActions()
            },
            initElements: function () {
                this.updateElementList();
                var a, c = !1;
                for (a = 0; a < this.elements.length; a += 1) this.options.disableEditing || this.elements[a].getAttribute("data-disable-editing") || this.elements[a].setAttribute("contentEditable", !0), this.elements[a].getAttribute("data-placeholder") || this.elements[a].setAttribute("data-placeholder", this.options.placeholder), this.elements[a].setAttribute("data-medium-element", !0), this.bindParagraphCreation(a).bindReturn(a).bindTab(a), this.options.disableToolbar || this.elements[a].getAttribute("data-disable-toolbar") || (c = !0);
                return c && (this.options.elementsContainer || (this.options.elementsContainer = b.body), this.initToolbar().bindButtons().bindAnchorForm().bindAnchorPreview()), this
            },
            setElementSelection: function (a) {
                this.elementSelection = a, this.updateElementList()
            },
            updateElementList: function () {
                this.elements = "string" == typeof this.elementSelection ? b.querySelectorAll(this.elementSelection) : this.elementSelection, 1 === this.elements.nodeType && (this.elements = [this.elements])
            },
            serialize: function () {
                var a, b, c = {};
                for (a = 0; a < this.elements.length; a += 1) b = "" !== this.elements[a].id ? this.elements[a].id : "element-" + a, c[b] = {
                    value: this.elements[a].innerHTML.trim()
                };
                return c
            },
            callExtensions: function (a) {
                if (!(arguments.length < 1)) {
                    var b, c, d = Array.prototype.slice.call(arguments, 1);
                    for (c in this.options.extensions) this.options.extensions.hasOwnProperty(c) && (b = this.options.extensions[c], void 0 !== b[a] && b[a].apply(b, d))
                }
            },
            bindParagraphCreation: function (a) {
                var c = this;
                return this.elements[a].addEventListener("keypress", function (a) {
                    var c, d = f();
                    32 === a.which && (c = d.tagName.toLowerCase(), "a" === c && b.execCommand("unlink", !1, null))
                }), this.elements[a].addEventListener("keyup", function (a) {
                    var d, e = f();
                    e && e.getAttribute("data-medium-element") && 0 === e.children.length && !c.options.disableReturn && !e.getAttribute("data-disable-return") && b.execCommand("formatBlock", !1, "p"), 13 === a.which && (e = f(), d = e.tagName.toLowerCase(), c.options.disableReturn || this.getAttribute("data-disable-return") || "li" === d || c.isListItemChild(e) || (a.shiftKey || b.execCommand("formatBlock", !1, "p"), "a" === d && b.execCommand("unlink", !1, null)))
                }), this
            },
            isListItemChild: function (a) {
                for (var b = a.parentNode, c = b.tagName.toLowerCase(); - 1 === this.parentElements.indexOf(c) && "div" !== c;) {
                    if ("li" === c) return !0;
                    if (b = b.parentNode, !b || !b.tagName) return !1;
                    c = b.tagName.toLowerCase()
                }
                return !1
            },
            bindReturn: function (a) {
                var b = this;
                return this.elements[a].addEventListener("keypress", function (a) {
                    if (13 === a.which)
                        if (b.options.disableReturn || this.getAttribute("data-disable-return")) a.preventDefault();
                        else if (b.options.disableDoubleReturn || this.getAttribute("data-disable-double-return")) {
                            var c = f();
                            c && "\n" === c.innerText && a.preventDefault()
                        }
                }), this
            },
            bindTab: function (a) {
                return this.elements[a].addEventListener("keydown", function (a) {
                    if (9 === a.which) {
                        var c = f().tagName.toLowerCase();
                        "pre" === c && (a.preventDefault(), b.execCommand("insertHtml", null, "    "))
                    }
                }), this
            },
            buttonTemplate: function (a) {
                var b = this.getButtonLabels(this.options.buttonLabels),
                    c = {
                        bold: '<button class="medium-editor-action medium-editor-action-bold" data-action="bold" data-element="b">' + b.bold + "</button>",
                        italic: '<button class="medium-editor-action medium-editor-action-italic" data-action="italic" data-element="i">' + b.italic + "</button>",
                        underline: '<button class="medium-editor-action medium-editor-action-underline" data-action="underline" data-element="u">' + b.underline + "</button>",
                        strikethrough: '<button class="medium-editor-action medium-editor-action-strikethrough" data-action="strikethrough" data-element="strike"><strike>A</strike></button>',
                        superscript: '<button class="medium-editor-action medium-editor-action-superscript" data-action="superscript" data-element="sup">' + b.superscript + "</button>",
                        subscript: '<button class="medium-editor-action medium-editor-action-subscript" data-action="subscript" data-element="sub">' + b.subscript + "</button>",
                        anchor: '<button class="medium-editor-action medium-editor-action-anchor" data-action="anchor" data-element="a">' + b.anchor + "</button>",
                        image: '<button class="medium-editor-action medium-editor-action-image" data-action="image" data-element="img">' + b.image + "</button>",
                        header1: '<button class="medium-editor-action medium-editor-action-header1" data-action="append-' + this.options.firstHeader + '" data-element="' + this.options.firstHeader + '">' + b.header1 + "</button>",
                        header2: '<button class="medium-editor-action medium-editor-action-header2" data-action="append-' + this.options.secondHeader + '" data-element="' + this.options.secondHeader + '">' + b.header2 + "</button>",
                        quote: '<button class="medium-editor-action medium-editor-action-quote" data-action="append-blockquote" data-element="blockquote">' + b.quote + "</button>",
                        orderedlist: '<button class="medium-editor-action medium-editor-action-orderedlist" data-action="insertorderedlist" data-element="ol">' + b.orderedlist + "</button>",
                        unorderedlist: '<button class="medium-editor-action medium-editor-action-unorderedlist" data-action="insertunorderedlist" data-element="ul">' + b.unorderedlist + "</button>",
                        pre: '<button class="medium-editor-action medium-editor-action-pre" data-action="append-pre" data-element="pre">' + b.pre + "</button>",
                        indent: '<button class="medium-editor-action medium-editor-action-indent" data-action="indent" data-element="ul">' + b.indent + "</button>",
                        outdent: '<button class="medium-editor-action medium-editor-action-outdent" data-action="outdent" data-element="ul">' + b.outdent + "</button>"
                    };
                return c[a] || !1
            },
            getButtonLabels: function (a) {
                var b, c, d = {
                    bold: "<b>B</b>",
                    italic: "<b><i>I</i></b>",
                    underline: "<b><u>U</u></b>",
                    superscript: "<b>x<sup>1</sup></b>",
                    subscript: "<b>x<sub>1</sub></b>",
                    anchor: "<b>#</b>",
                    image: "<b>image</b>",
                    header1: "<b>H2</b>",
                    header2: "<b>H3</b>",
                    quote: "<b>&ldquo;</b>",
                    orderedlist: "<b>1.</b>",
                    unorderedlist: "<b>&bull;</b>",
                    pre: "<b>0101</b>",
                    indent: "<b>&rarr;</b>",
                    outdent: "<b>&larr;</b>"
                };
                if ("fontawesome" === a ? b = {
                    bold: '<i class="fa fa-bold"></i>',
                    italic: '<i class="fa fa-italic"></i>',
                    underline: '<i class="fa fa-underline"></i>',
                    superscript: '<i class="fa fa-superscript"></i>',
                    subscript: '<i class="fa fa-subscript"></i>',
                    anchor: '<i class="fa fa-link"></i>',
                    image: '<i class="fa fa-picture-o"></i>',
                    quote: '<i class="fa fa-quote-right"></i>',
                    orderedlist: '<i class="fa fa-list-ol"></i>',
                    unorderedlist: '<i class="fa fa-list-ul"></i>',
                    pre: '<i class="fa fa-code fa-lg"></i>',
                    indent: '<i class="fa fa-indent"></i>',
                    outdent: '<i class="fa fa-outdent"></i>'
                } : "object" == typeof a && (b = a), "object" == typeof b)
                    for (c in b) b.hasOwnProperty(c) && (d[c] = b[c]);
                return d
            },
            initToolbar: function () {
                return this.toolbar ? this : (this.toolbar = this.createToolbar(), this.keepToolbarAlive = !1, this.anchorForm = this.toolbar.querySelector(".medium-editor-toolbar-form-anchor"), this.anchorInput = this.anchorForm.querySelector("input"), this.toolbarActions = this.toolbar.querySelector(".medium-editor-toolbar-actions"), this.anchorPreview = this.createAnchorPreview(), this)
            },
            createToolbar: function () {
                var a = b.createElement("div");
                return a.id = "medium-editor-toolbar-" + this.id, a.className = "medium-editor-toolbar", a.appendChild(this.toolbarButtons()), a.appendChild(this.toolbarFormAnchor()), this.options.elementsContainer.appendChild(a), a
            },
            toolbarButtons: function () {
                var a, c, d, e, f = this.options.buttons,
                    g = b.createElement("ul");
                for (g.id = "medium-editor-toolbar-actions", g.className = "medium-editor-toolbar-actions clearfix", c = 0; c < f.length; c += 1) this.options.extensions.hasOwnProperty(f[c]) ? (e = this.options.extensions[f[c]], d = void 0 !== e.getButton ? e.getButton() : null) : d = this.buttonTemplate(f[c]), d && (a = b.createElement("li"), h(d) ? a.appendChild(d) : a.innerHTML = d, g.appendChild(a));
                return g
            },
            toolbarFormAnchor: function () {
                var a = b.createElement("div"),
                    c = b.createElement("input"),
                    d = b.createElement("a");
                return d.setAttribute("href", "#"), d.innerHTML = "&times;", c.setAttribute("type", "text"), c.setAttribute("placeholder", this.options.anchorInputPlaceholder), a.className = "medium-editor-toolbar-form-anchor", a.id = "medium-editor-toolbar-form-anchor", a.appendChild(c), a.appendChild(d), a
            },
            bindSelect: function () {
                var a, c = this,
                    d = "";
                for (this.checkSelectionWrapper = function (a) {
                    return a && c.clickingIntoArchorForm(a) ? !1 : (clearTimeout(d), void (d = setTimeout(function () {
                        c.checkSelection()
                    }, c.options.delay)))
                }, b.documentElement.addEventListener("mouseup", this.checkSelectionWrapper), a = 0; a < this.elements.length; a += 1) this.elements[a].addEventListener("keyup", this.checkSelectionWrapper), this.elements[a].addEventListener("blur", this.checkSelectionWrapper);
                return this
            },
            checkSelection: function () {
                var b, c;
                return this.keepToolbarAlive === !0 || this.options.disableToolbar || (b = a.getSelection(), "" === b.toString().trim() || this.options.allowMultiParagraphSelection === !1 && this.hasMultiParagraphs() ? this.hideToolbarActions() : (c = this.getSelectionElement(), !c || c.getAttribute("data-disable-toolbar") ? this.hideToolbarActions() : this.checkSelectionElement(b, c))), this
            },
            clickingIntoArchorForm: function (a) {
                var b = this;
                return a.type && "blur" === a.type.toLowerCase() && a.relatedTarget && a.relatedTarget === b.anchorInput ? !0 : !1
            },
            hasMultiParagraphs: function () {
                var a = g().replace(/<[\S]+><\/[\S]+>/gim, ""),
                    b = a.match(/<(p|h[0-6]|blockquote)>([\s\S]*?)<\/(p|h[0-6]|blockquote)>/g);
                return b ? b.length : 0
            },
            checkSelectionElement: function (a, b) {
                var c;
                for (this.selection = a, this.selectionRange = this.selection.getRangeAt(0), c = 0; c < this.elements.length; c += 1)
                    if (this.elements[c] === b) return void this.setToolbarButtonStates().setToolbarPosition().showToolbarActions();
                this.hideToolbarActions()
            },
            getSelectionElement: function () {
                var b, c, d, e, f = a.getSelection(),
                    g = function (a) {
                        var b = a;
                        try {
                            for (; !b.getAttribute("data-medium-element");) b = b.parentNode
                        } catch (c) {
                            return !1
                        }
                        return b
                    };
                try {
                    b = f.getRangeAt(0), c = b.commonAncestorContainer, d = c.parentNode, e = c.getAttribute("data-medium-element") ? c : g(d)
                } catch (h) {
                    e = g(d)
                }
                return e
            },
            setToolbarPosition: function () {
                var b = 50,
                    c = a.getSelection(),
                    d = c.getRangeAt(0),
                    e = d.getBoundingClientRect(),
                    f = this.options.diffLeft - this.toolbar.offsetWidth / 2,
                    g = (e.left + e.right) / 2,
                    h = this.toolbar.offsetWidth / 2;
                return e.top < b ? (this.toolbar.classList.add("medium-toolbar-arrow-over"), this.toolbar.classList.remove("medium-toolbar-arrow-under"), this.toolbar.style.top = b + e.bottom - this.options.diffTop + a.pageYOffset - this.toolbar.offsetHeight + "px") : (this.toolbar.classList.add("medium-toolbar-arrow-under"), this.toolbar.classList.remove("medium-toolbar-arrow-over"), this.toolbar.style.top = e.top + this.options.diffTop + a.pageYOffset - this.toolbar.offsetHeight + "px"), this.toolbar.style.left = h > g ? f + h + "px" : a.innerWidth - g < h ? a.innerWidth + f - h + "px" : f + g + "px", this.hideAnchorPreview(), this
            },
            setToolbarButtonStates: function () {
                var a, b = this.toolbarActions.querySelectorAll("button");
                for (a = 0; a < b.length; a += 1) b[a].classList.remove(this.options.activeButtonClass);
                return this.checkActiveButtons(), this
            },
            checkActiveButtons: function () {
                for (var a = Array.prototype.slice.call(this.elements), b = this.getSelectedParentElement(); void 0 !== b.tagName && -1 === this.parentElements.indexOf(b.tagName.toLowerCase) && (this.activateButton(b.tagName.toLowerCase()), this.callExtensions("checkState", b), -1 === a.indexOf(b));) b = b.parentNode
            },
            activateButton: function (a) {
                var b = this.toolbar.querySelector('[data-element="' + a + '"]');
                null !== b && -1 === b.className.indexOf(this.options.activeButtonClass) && (b.className += " " + this.options.activeButtonClass)
            },
            bindButtons: function () {
                var a, b = this.toolbar.querySelectorAll("button"),
                    c = this,
                    d = function (a) {
                        a.preventDefault(), a.stopPropagation(), void 0 === c.selection && c.checkSelection(), this.className.indexOf(c.options.activeButtonClass) > -1 ? this.classList.remove(c.options.activeButtonClass) : this.className += " " + c.options.activeButtonClass, this.hasAttribute("data-action") && c.execAction(this.getAttribute("data-action"), a)
                    };
                for (a = 0; a < b.length; a += 1) b[a].addEventListener("click", d);
                return this.setFirstAndLastItems(b), this
            },
            setFirstAndLastItems: function (a) {
                return a.length > 0 && (a[0].className += " " + this.options.firstButtonClass, a[a.length - 1].className += " " + this.options.lastButtonClass), this
            },
            execAction: function (c, d) {
                c.indexOf("append-") > -1 ? (this.execFormatBlock(c.replace("append-", "")), this.setToolbarPosition(), this.setToolbarButtonStates()) : "anchor" === c ? this.triggerAnchorAction(d) : "image" === c ? b.execCommand("insertImage", !1, a.getSelection()) : (b.execCommand(c, !1, null), this.setToolbarPosition())
            },
            rangeSelectsSingleNode: function (a) {
                var b = a.startContainer;
                return b === a.endContainer && b.hasChildNodes() && a.endOffset === a.startOffset + 1
            },
            getSelectedParentElement: function () {
                var a = null,
                    b = this.selectionRange;
                return a = this.rangeSelectsSingleNode(b) ? b.startContainer.childNodes[b.startOffset] : 3 === b.startContainer.nodeType ? b.startContainer.parentNode : b.startContainer
            },
            triggerAnchorAction: function () {
                var a = this.getSelectedParentElement();
                return a.tagName && "a" === a.tagName.toLowerCase() ? b.execCommand("unlink", !1, null) : "block" === this.anchorForm.style.display ? this.showToolbarActions() : this.showAnchorForm(), this
            },
            execFormatBlock: function (a) {
                var c = this.getSelectionData(this.selection.anchorNode);
                if ("blockquote" === a && c.el && "blockquote" === c.el.parentNode.tagName.toLowerCase()) return b.execCommand("outdent", !1, null);
                if (c.tagName === a && (a = "p"), this.isIE) {
                    if ("blockquote" === a) return b.execCommand("indent", !1, a);
                    a = "<" + a + ">"
                }
                return b.execCommand("formatBlock", !1, a)
            },
            getSelectionData: function (a) {
                var b;
                for (a && a.tagName && (b = a.tagName.toLowerCase()); a && -1 === this.parentElements.indexOf(b);) a = a.parentNode, a && a.tagName && (b = a.tagName.toLowerCase());
                return {
                    el: a,
                    tagName: b
                }
            },
            getFirstChild: function (a) {
                for (var b = a.firstChild; null !== b && 1 !== b.nodeType;) b = b.nextSibling;
                return b
            },
            hideToolbarActions: function () {
                this.keepToolbarAlive = !1, void 0 !== this.toolbar && this.toolbar.classList.remove("medium-editor-toolbar-active")
            },
            showToolbarActions: function () {
                var a, b = this;
                this.anchorForm.style.display = "none", this.toolbarActions.style.display = "block", this.keepToolbarAlive = !1, clearTimeout(a), a = setTimeout(function () {
                    b.toolbar && !b.toolbar.classList.contains("medium-editor-toolbar-active") && b.toolbar.classList.add("medium-editor-toolbar-active")
                }, 100)
            },
            saveSelection: function () {
                this.savedSelection = d()
            },
            restoreSelection: function () {
                e(this.savedSelection)
            },
            showAnchorForm: function (a) {
                this.toolbarActions.style.display = "none", this.saveSelection(), this.anchorForm.style.display = "block", this.keepToolbarAlive = !0, this.anchorInput.focus(), this.anchorInput.value = a || ""
            },
            bindAnchorForm: function () {
                var a = this.anchorForm.querySelector("a"),
                    b = this;
                return this.anchorForm.addEventListener("click", function (a) {
                    a.stopPropagation()
                }), this.anchorInput.addEventListener("keyup", function (a) {
                    13 === a.keyCode && (a.preventDefault(), b.createLink(this))
                }), this.anchorInput.addEventListener("click", function (a) {
                    a.stopPropagation(), b.keepToolbarAlive = !0
                }), this.anchorInput.addEventListener("blur", function () {
                    b.keepToolbarAlive = !1, b.checkSelection()
                }), a.addEventListener("click", function (a) {
                    a.preventDefault(), b.showToolbarActions(), e(b.savedSelection)
                }), this
            },
            hideAnchorPreview: function () {
                this.anchorPreview.classList.remove("medium-editor-anchor-preview-active")
            },
            showAnchorPreview: function (b) {
                if (this.anchorPreview.classList.contains("medium-editor-anchor-preview-active")) return !0;
                var c, d, e, f = this,
                    g = 40,
                    h = b.getBoundingClientRect(),
                    i = (h.left + h.right) / 2;
                return f.anchorPreview.querySelector("i").textContent = b.href, c = f.anchorPreview.offsetWidth / 2, d = f.options.diffLeft - c, clearTimeout(e), e = setTimeout(function () {
                    f.anchorPreview && !f.anchorPreview.classList.contains("medium-editor-anchor-preview-active") && f.anchorPreview.classList.add("medium-editor-anchor-preview-active")
                }, 100), f.observeAnchorPreview(b), f.anchorPreview.classList.add("medium-toolbar-arrow-over"), f.anchorPreview.classList.remove("medium-toolbar-arrow-under"), f.anchorPreview.style.top = Math.round(g + h.bottom - f.options.diffTop + a.pageYOffset - f.anchorPreview.offsetHeight) + "px", f.anchorPreview.style.left = c > i ? d + c + "px" : a.innerWidth - i < c ? a.innerWidth + d - c + "px" : d + i + "px", this
            },
            observeAnchorPreview: function (a) {
                var b = this,
                    c = (new Date).getTime(),
                    d = !0,
                    e = function () {
                        c = (new Date).getTime(), d = !0
                    },
                    f = function (a) {
                        a.relatedTarget && /anchor-preview/.test(a.relatedTarget.className) || (d = !1)
                    },
                    g = setInterval(function () {
                        if (d) return !0;
                        var h = (new Date).getTime() - c;
                        h > b.options.anchorPreviewHideDelay && (b.hideAnchorPreview(), clearInterval(g), b.anchorPreview.removeEventListener("mouseover", e), b.anchorPreview.removeEventListener("mouseout", f), a.removeEventListener("mouseover", e), a.removeEventListener("mouseout", f))
                    }, 200);
                b.anchorPreview.addEventListener("mouseover", e), b.anchorPreview.addEventListener("mouseout", f), a.addEventListener("mouseover", e), a.addEventListener("mouseout", f)
            },
            createAnchorPreview: function () {
                var a = this,
                    c = b.createElement("div");
                return c.id = "medium-editor-anchor-preview-" + this.id, c.className = "medium-editor-anchor-preview", c.innerHTML = this.anchorPreviewTemplate(), this.options.elementsContainer.appendChild(c), c.addEventListener("click", function () {
                    a.anchorPreviewClickHandler()
                }), c
            },
            anchorPreviewTemplate: function () {
                return '<div class="medium-editor-toolbar-anchor-preview" id="medium-editor-toolbar-anchor-preview">    <i class="medium-editor-toolbar-anchor-preview-inner"></i></div>'
            },
            anchorPreviewClickHandler: function () {
                if (this.activeAnchor) {
                    var c = this,
                        d = b.createRange(),
                        e = a.getSelection();
                    d.selectNodeContents(c.activeAnchor), e.removeAllRanges(), e.addRange(d), setTimeout(function () {
                        c.activeAnchor && c.showAnchorForm(c.activeAnchor.href), c.keepToolbarAlive = !1
                    }, 100 + c.options.delay)
                }
                this.hideAnchorPreview()
            },
            editorAnchorObserver: function (a) {
                var b = this,
                    c = !0,
                    d = function () {
                        c = !1, b.activeAnchor.removeEventListener("mouseout", d)
                    };
                if (a.target && "a" === a.target.tagName.toLowerCase()) {
                    if (!/href=["']\S+["']/.test(a.target.outerHTML) || /href=["']#\S+["']/.test(a.target.outerHTML)) return !0;
                    if (this.toolbar.classList.contains("medium-editor-toolbar-active")) return !0;
                    this.activeAnchor = a.target, this.activeAnchor.addEventListener("mouseout", d), setTimeout(function () {
                        c && b.showAnchorPreview(a.target)
                    }, b.options.delay)
                }
            },
            bindAnchorPreview: function () {
                var a, b = this;
                for (this.editorAnchorObserverWrapper = function (a) {
                    b.editorAnchorObserver(a)
                }, a = 0; a < this.elements.length; a += 1) this.elements[a].addEventListener("mouseover", this.editorAnchorObserverWrapper);
                return this
            },
            checkLinkFormat: function (a) {
                var b = /^(https?|ftps?|rtmpt?):\/\/|mailto:/;
                return (b.test(a) ? "" : "http://") + a
            },
            setTargetBlank: function () {
                var a, b = f();
                if ("a" === b.tagName.toLowerCase()) b.target = "_blank";
                else
                    for (b = b.getElementsByTagName("a"), a = 0; a < b.length; a += 1) b[a].target = "_blank"
            },
            createLink: function (a) {
                return 0 === a.value.trim().length ? void this.hideToolbarActions() : (e(this.savedSelection), this.options.checkLinkFormat && (a.value = this.checkLinkFormat(a.value)), b.execCommand("createLink", !1, a.value), this.options.targetBlank && this.setTargetBlank(), this.checkSelection(), this.showToolbarActions(), void (a.value = ""))
            },
            bindWindowActions: function () {
                var b, c = this;
                return this.windowResizeHandler = function () {
                    clearTimeout(b), b = setTimeout(function () {
                        c.toolbar && c.toolbar.classList.contains("medium-editor-toolbar-active") && c.setToolbarPosition()
                    }, 100)
                }, a.addEventListener("resize", this.windowResizeHandler), this
            },
            activate: function () {
                this.isActive || this.setup()
            },
            deactivate: function () {
                var c;
                if (this.isActive)
                    for (this.isActive = !1, void 0 !== this.toolbar && (this.options.elementsContainer.removeChild(this.anchorPreview), this.options.elementsContainer.removeChild(this.toolbar), delete this.toolbar, delete this.anchorPreview), b.documentElement.removeEventListener("mouseup", this.checkSelectionWrapper), a.removeEventListener("resize", this.windowResizeHandler), c = 0; c < this.elements.length; c += 1) this.elements[c].removeEventListener("mouseover", this.editorAnchorObserverWrapper), this.elements[c].removeEventListener("keyup", this.checkSelectionWrapper), this.elements[c].removeEventListener("blur", this.checkSelectionWrapper), this.elements[c].removeEventListener("paste", this.pasteWrapper), this.elements[c].removeAttribute("contentEditable"), this.elements[c].removeAttribute("data-medium-element")
            },
            htmlEntities: function (a) {
                return String(a).replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;")
            },
            bindPaste: function () {
                var a, c = this;
                for (this.pasteWrapper = function (a) {
                    var d, e, f = "";
                    if (this.classList.remove("medium-editor-placeholder"), !c.options.forcePlainText && !c.options.cleanPastedHTML) return this;
                    if (a.clipboardData && a.clipboardData.getData && !a.defaultPrevented) {
                        if (a.preventDefault(), c.options.cleanPastedHTML && a.clipboardData.getData("text/html")) return c.cleanPaste(a.clipboardData.getData("text/html"));
                        if (c.options.disableReturn || this.getAttribute("data-disable-return")) b.execCommand("insertHTML", !1, a.clipboardData.getData("text/plain"));
                        else {
                            for (d = a.clipboardData.getData("text/plain").split(/[\r\n]/g), e = 0; e < d.length; e += 1) "" !== d[e] && (f += navigator.userAgent.match(/firefox/i) && 0 === e ? c.htmlEntities(d[e]) : "<p>" + c.htmlEntities(d[e]) + "</p>");
                            b.execCommand("insertHTML", !1, f)
                        }
                    }
                }, a = 0; a < this.elements.length; a += 1) this.elements[a].addEventListener("paste", this.pasteWrapper);
                return this
            },
            setPlaceholders: function () {
                var a, b = function (a) {
                    a.querySelector("img") || a.querySelector("blockquote") || "" !== a.textContent.replace(/^\s+|\s+$/g, "") || a.classList.add("medium-editor-placeholder")
                },
                    c = function (a) {
                        this.classList.remove("medium-editor-placeholder"), "keypress" !== a.type && b(this)
                    };
                for (a = 0; a < this.elements.length; a += 1) b(this.elements[a]), this.elements[a].addEventListener("blur", c), this.elements[a].addEventListener("keypress", c);
                return this
            },
            cleanPaste: function (a) {
                var c, d, e, f = this.getSelectionElement(),
                    g = /<p|<br|<div/.test(a),
                    h = [
                        [new RegExp(/<[^>]*docs-internal-guid[^>]*>/gi), ""],
                        [new RegExp(/<\/b>(<br[^>]*>)?$/gi), ""],
                        [new RegExp(/<span class="Apple-converted-space">\s+<\/span>/g), " "],
                        [new RegExp(/<br class="Apple-interchange-newline">/g), "<br>"],
                        [new RegExp(/<span[^>]*(font-style:italic;font-weight:bold|font-weight:bold;font-style:italic)[^>]*>/gi), '<span class="replace-with italic bold">'],
                        [new RegExp(/<span[^>]*font-style:italic[^>]*>/gi), '<span class="replace-with italic">'],
                        [new RegExp(/<span[^>]*font-weight:bold[^>]*>/gi), '<span class="replace-with bold">'],
                        [new RegExp(/&lt;(\/?)(i|b|a)&gt;/gi), "<$1$2>"],
                        [new RegExp(/&lt;a\s+href=(&quot;|&rdquo;|&ldquo;|“|”)([^&]+)(&quot;|&rdquo;|&ldquo;|“|”)&gt;/gi), '<a href="$2">']
                    ];
                for (c = 0; c < h.length; c += 1) a = a.replace(h[c][0], h[c][1]);
                if (g)
                    for (d = a.split("<br><br>"), this.pasteHTML("<p>" + d.join("</p><p>") + "</p>"), b.execCommand("insertText", !1, "\n"), d = f.querySelectorAll("p,div,br"), c = 0; c < d.length; c += 1) switch (e = d[c], e.tagName.toLowerCase()) {
                        case "p":
                        case "div":
                            this.filterCommonBlocks(e);
                            break;
                        case "br":
                            this.filterLineBreak(e)
                    } else this.pasteHTML(a)
            },
            pasteHTML: function (a) {
                var c, d, e, f, g = b.createDocumentFragment();
                for (g.appendChild(b.createElement("body")), f = g.querySelector("body"), f.innerHTML = a, this.cleanupSpans(f), c = f.querySelectorAll("*"), e = 0; e < c.length; e += 1) d = c[e], d.removeAttribute("class"), d.removeAttribute("style"), d.removeAttribute("dir"), "meta" === d.tagName.toLowerCase() && d.parentNode.removeChild(d);
                b.execCommand("insertHTML", !1, f.innerHTML.replace(/&nbsp;/g, " "))
            },
            isCommonBlock: function (a) {
                return a && ("p" === a.tagName.toLowerCase() || "div" === a.tagName.toLowerCase())
            },
            filterCommonBlocks: function (a) {
                /^\s*$/.test(a.innerText) && a.parentNode.removeChild(a)
            },
            filterLineBreak: function (a) {
                this.isCommonBlock(a.previousElementSibling) ? a.parentNode.removeChild(a) : !this.isCommonBlock(a.parentNode) || a.parentNode.firstChild !== a && a.parentNode.lastChild !== a ? 1 === a.parentNode.childElementCount && this.removeWithParent(a) : a.parentNode.removeChild(a)
            },
            removeWithParent: function (a) {
                a && a.parentNode && (a.parentNode.parentNode && 1 === a.parentNode.childElementCount ? a.parentNode.parentNode.removeChild(a.parentNode) : a.parentNode.removeChild(a.parentNode))
            },
            cleanupSpans: function (a) {
                var c, d, e, f = a.querySelectorAll(".replace-with");
                for (c = 0; c < f.length; c += 1) d = f[c], e = b.createElement(d.classList.contains("bold") ? "b" : "i"), e.innerHTML = d.classList.contains("bold") && d.classList.contains("italic") ? "<i>" + d.innerHTML + "</i>" : d.innerHTML, d.parentNode.replaceChild(e, d);
                for (f = a.querySelectorAll("span"), c = 0; c < f.length; c += 1) d = f[c], /^\s*$/.test() ? d.parentNode.removeChild(d) : d.parentNode.replaceChild(b.createTextNode(d.innerText), d)
            }
        }
    }(window, document);