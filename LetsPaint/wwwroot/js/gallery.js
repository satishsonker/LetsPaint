$(document).ready(function () {
    var $galleriesContainer = $('.galleries ul'), $galleriesList = '', $query = getUrlVars(), $galId = parseInt($query.galId);
    $($galleriesContainer).empty();
    $(galleries).each(function (ind, ele) {
        $galleriesList += `<li data-id="${ele.id}" class="${ele.id === $galId ? 'active' : ''}" data-baseurl="${ele.baseUrl}">${ele.name}</li>`;
    });
    $($galleriesContainer).append($galleriesList);
    bindGallery($galId === undefined || isNaN($galId) ? galleries[0].id : $galId);
    if (isNaN($galId)) {
        $('.galleries ul li').eq(0).addClass('active');
    }

});
$(document).on('click', '.galleries ul li', function () {
    var $id = parseInt($(this).data('id'));
    bindGallery($id);
    $('.galleries ul li').removeClass('active');
    $(this).addClass('active');
});

function bindGallery($galId) {
    var $galData = galleries.filter(x => x.id === $galId);
    var $container = $('.product-section .container');
    $($container).find('.row:gt(0)').remove();
    var $list = ``;
    $(galleryData.filter(x => x.galleryId === $galData[0].id)).each(function (ind, ele) {
        if (ind === 0) {
            $list += '<div class="row">';
        } if (ind % $galData[0].gridSize === 0 && ind !== 0) {
            $list += '</div><div class="row">';
        }
        $list += `<div class="col-md-3">
               
                    <div class="gallery-container">
                       <a href="/home/theartist">
<div class="overlay-artist">Artist - ${ele.artist}</div> </a>
<a class="gallery-item" href="${$galData[0].baseUrl}${ele.image}" data-lightbox="mygallery">
                        <img src="${$galData[0].baseUrl}${ele.thumbnail}" class="image" />
  </a>
                        <div class="overlay">${ele.title}</div>
                    </div>
              
            </div>`;

    });
    $($container).append($list + '</div>');
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