/// <reference path="common/apiurls.js" />
$(document).ready(function () {
    var $galleriesContainer = $('.galleries ul'), $galleriesList = '', $query = getUrlVars(), $galId = parseInt($query.galId);
    $($galleriesContainer).empty();

    api.get(url.root.gallery.getgalleryType).then(function (data) {
        galleries = data.data;
        $(galleries).each(function (ind, ele) {
            $galleriesList += `<li data-id="${ele.galleryTypeId}" data-gridsize="${ele.gridSize}" class="${ele.galleryTypeId === $galId ? 'active' : ''}" data-baseurl="${ele.baseUrl}">${ele.galleryType}</li>`;
        });
        $($galleriesContainer).append($galleriesList);
        bindGallery($galId === undefined || isNaN($galId) ? galleries[0].galleryTypeId : $galId);
        if (isNaN($galId)) {
            $('.galleries ul li').eq(0).addClass('active');
        }
    })
   

});
$(document).on('click', '.galleries ul li', function () {
    var $id = parseInt($(this).data('id'));
    bindGallery($id);
    $('.galleries ul li').removeClass('active');
    $(this).addClass('active');
});

function bindGallery($galId) {
    var $galData = galleries.filter(x => x.galleryTypeId === $galId);
    var $container = $('.product-section .container');
    $($container).find('.row:gt(0)').remove();
    var $list = ``;
    apiGet(url.root.gallery.getGallery + `?galleryTypeId=${$galId}`).then(function (data) {
        if (data.data.records.length > 0) {
            $(data.data.records).each(function (ind, ele) {
                if (ind === 0) {
                    $list += '<div class="row">';
                } if (ind % $galData[0].gridSize === 0 && ind !== 0) {
                    $list += '</div><div class="row">';
                }
                $list += `<div class="col-md-3 p-col">               
                    <div class="gallery-container">
                    <div class="new-badge">${ele.badge === null || ele.badge === 'null' ? '' : ele.badge}</div>
                       <a href="/home/theartist"><div class="overlay-artist">Artist - ${ele.artist}</div> </a>
                        <a class="gallery-item" href="${$galData[0].baseUrl}${ele.image}" data-lightbox="mygallery">
                        <img src="${$galData[0].baseUrl}${ele.thumbnail}" class="image" /> </a>
                        <div class="overlay">${ele.title} 
                            <div class="img-ctrl">
                                <i class="fas fa-heart"><span>0</span></i>
                                <i class="fas fa-comment-alt"><span>0</span></i>
                                <i class="fas fa-comment"></i>
                            </div>
                        </div>

                    </div>
              
            </div>`;

            });
            $($container).append($list + '</div>');
        }
        else {
            $($container).append(` <div class="nodata row">
            <h4>Artist is painting their Creativity</h4>
<h6>They will come soon here</h6>
            <img  src="/images/workingArtist.gif" />
        </div>`);
        }
    });
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}