/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */


CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here.
    // For complete reference see:
    // http://docs.ckeditor.com/#!/api/CKEDITOR.config

    config.language = 'cs';

    // The toolbar groups arrangement, optimized for two toolbar rows.
    config.toolbarGroups = [
        { name: 'clipboard', groups: ['clipboard', 'undo'] },
        { name: 'editing', groups: ['find', 'selection', 'spellchecker'] },
        { name: 'links' },
        { name: 'insert' },
        { name: 'forms' },
        { name: 'tools' },
        { name: 'document', groups: ['mode', 'document', 'doctools'] },
        { name: 'about' },
        '/',
        { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
        { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'] },
        { name: 'styles' },
        { name: 'colors' }
    ];

    // config.contentsCss = "https://fonts.googleapis.com/css?family=Open+Sans:400,400i,700,700i&subset=latin-ext";

    config.font_names = 'Open Sans;' + config.font_names;
    config.height = 400;

    config.filebrowserUploadUrl = window.location.origin + '/api/image';
    config.extraPlugins = 'filebrowser';
    config.extraPlugins = 'image2';
    config.removePlugins = 'emojione';

    config.image2_alignClasses = ['ck-left', 'ck-center', 'ck-right'];

    config.font_defaultLabel = 'Open Sans';
    config.fontSize_defaultLabel = '14';
    config.line_height_defaultLabel = '1.4';
    config.color_defaultLabel = '5d5e5e';

    // Set the most common block elements.
    config.format_tags = 'p;h1;h2;h3;pre';

    // Simplify the dialog windows.
    config.removeDialogTabs = 'image:advanced;link:advanced';
};

