// Search
(function () {
    $(document).on('keyup', '#SearchInput', function () {
        var searchString = this.value.toLowerCase();
        if ($('.sidebar').width() < 100) {
            searchString = '';
        }

        var menus = $('.sidebar-navigation li');
        for (var i = 0; i < menus.length; i++) {
            var menu = $(menus[i]);
            if (menu.text().toLowerCase().indexOf(searchString) >= 0) {
                if (menu.hasClass('submenu')) {
                    if (menu.find('li:not(.submenu)').text().toLowerCase().indexOf(searchString) >= 0) {
                        menu.show(500);
                    } else {
                        menu.hide(500);
                    }
                } else {
                    menu.show(500);
                }
            } else {
                menu.hide(500);
            }
        }
    });
}());

// Hovering
(function () {
    $(document).on('mouseenter', '.sidebar-navigation a', function () {
        var parent = $(this).parent();
        if (!parent.hasClass('active') && !parent.hasClass('inactive'))
            $('.sidebar-navigation .active').removeClass('active').addClass('inactive');
        else
            parent.removeClass('inactive').addClass('active');

        if (!parent.hasClass('has-active') && !parent.hasClass('has-active-hover'))
            $('.sidebar-navigation .has-active').removeClass('has-active').addClass('has-active-hover');
        else
            parent.removeClass('has-active-hover').addClass('has-active');
    });

    $(document).on('mouseleave', '.sidebar-navigation', function () {
        $('.sidebar-navigation .has-active-hover').removeClass('has-active-hover').addClass('has-active');
        $('.sidebar-navigation .inactive').removeClass('inactive').addClass('active');

        if ($('.sidebar').width() < 100) {
            var submenu = $('.sidebar-navigation li.open');
            submenu.toggleClass('closing');
            submenu.toggleClass('open');
            submenu.children('ul').fadeOut(200, function () {
                $(this).parent().toggleClass('closing');
            });
        }
    });
}());

// Clicking
(function () {
    $(document).on('click', '.submenu > a', function () {
        if ($(window).width() > 767)
            toggleSubmenu($(this));
        else
            toggleDropdown($(this));
    });

    function toggleSubmenu(submenuAction) {
        var submenu = submenuAction.parent();
        var openedMenu = submenu.siblings('.open');
        if (openedMenu.length > 0) {
            openedMenu.toggleClass('closing');
            openedMenu.toggleClass('open');
            openedMenu.children('ul').slideUp({
                complete: function () {
                    submenu.toggleClass('closing');
                }
            });
        }

        if (submenu.hasClass('open')) {
            submenu.toggleClass('closing');
            submenu.toggleClass('open');
            submenuAction.siblings('ul').slideUp({
                complete: function () {
                    submenu.toggleClass('closing');
                }
            });
        } else {
            submenu.toggleClass('opening');
            submenuAction.siblings('ul').slideDown({
                complete: function () {
                    submenu.toggleClass('open');
                    submenu.toggleClass('opening');
                }
            });
        }
    }
    function toggleDropdown(submenuAction) {
        var submenu = submenuAction.parent();
        var openedMenu = submenu.siblings('.open');
        if (openedMenu.length > 0) {
            openedMenu.toggleClass('closing');
            openedMenu.toggleClass('open');
            openedMenu.children('ul').fadeOut(200, function () {
                submenu.toggleClass('closing');
            });
        }

        if (submenu.hasClass('open')) {
            submenu.toggleClass('closing');
            submenu.toggleClass('open');
            submenuAction.siblings('ul').fadeOut(200, function () {
                submenu.toggleClass('closing');
            });
        } else {
            submenu.toggleClass('opening');
            submenuAction.siblings('ul').fadeIn(200, function () {
                submenu.toggleClass('open');
                submenu.toggleClass('opening');
            });
        }
    }
}());

// Rendering on low resolutions
(function () {
    if ($('.sidebar').width() < 100)
        $('.sidebar-navigation li.open').removeClass('open');

    $(window).on('resize', function () {
        if ($('.sidebar').width() < 100)
            $('.sidebar-navigation li.open').removeClass('open').children('ul').hide();
        else
            $('.sidebar-navigation li.has-active').addClass('open').children('ul').show();

        $('#SearchInput').keyup();
    });
}());
