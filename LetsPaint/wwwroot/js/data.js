﻿let galleries = [
    { id: 1, name: 'Painting', baseUrl: "/images/Product/Painting/", gridSize: 4 },
    { id: 2, name: 'Craft', baseUrl: "/images/Product/Craft/", gridSize: 4 },
    { id: 3, name: 'Rangoli', baseUrl: "/images/Product/Rangoli/", gridSize: 3 },
    { id: 4, name: 'Wall Painting', baseUrl: "/images/Product/WallPainting/", gridSize: 4 }
];
let galleryData = [
    { id: 1, galleryId: 1, title: "Madhubani Art", thumbnail: "14_sm.jpg", image: "14.jpeg", badge: "New",artist:"Poonam Sonkar"},
    { id: 2, galleryId: 1, title: "African Women", thumbnail: "1_sm.jpg", image: "1.jpeg", badge: "New",artist:"Poonam Sonkar" },
    { id: 3, galleryId: 1, title: "Birds", thumbnail: "2_sm.jpg", image: "2.jpg", badge: "New",artist:"Poonam Sonkar" },
    { id: 4, galleryId: 1, title: "Lord Ganesha", thumbnail: "3_sm.jpg", image: "3.jpg", badge: "New",artist:"Poonam Sonkar" },
    { id: 5, galleryId: 1, title: "Tribal Art/African", thumbnail: "4_sm.jpg", image: "4.jpg", badge: "New",artist:"Poonam Sonkar" },
    { id: 6, galleryId: 1, title: "Mother's special", thumbnail: "5_sm.jpg", image: "5.jpg", badge: "New",artist:"Poonam Sonkar" },
    { id: 7, galleryId: 1, title: "Lord Buddha", thumbnail: "6_sm.jpg", image: "6.jpg", badge: "New",artist:"Poonam Sonkar" },
    { id: 8, galleryId: 1, title: "Then & Now", thumbnail: "7_sm.jpg", image: "7.jpg", badge: "New",artist:"Poonam Sonkar" },
    { id:9, galleryId: 1, title: "Valentine's Special", thumbnail: "8_sm.jpg", image: "8.jpg", badge: "New",artist:"Poonam Sonkar" },
    { id:10, galleryId: 1, title: "Love in Air", thumbnail: "9_sm.jpg", image: "9.jpg", badge: "New",artist:"Poonam Sonkar" },
    { id: 11, galleryId: 1, title: "Wildlife", thumbnail: "10_sm.jpg", image: "10.jpg", badge: "New",artist:"Poonam Sonkar" },
    { id: 12, galleryId: 1, title: "Immortal Hanuman", thumbnail: "11_sm.jpg", image: "11.jpg", badge: "New",artist:"Poonam Sonkar" },
    { id: 12, galleryId: 1, title: "Floral Design", thumbnail: "12_sm.jpeg", image: "12.jpeg", badge: "New", artist: "Poonam Sonkar" },
    { id: 27, galleryId: 1, title: "Lord Kirshna", thumbnail: "15_sm.jpg", image: "15.jpg", badge: "New", artist: "Poonam Sonkar" },
    { id: 28, galleryId: 1, title: "Mordern Art", thumbnail: "16_sm.jpg", image: "16.jpg", badge: "New", artist: "Poonam Sonkar" },

    { id: 25, galleryId: 2, title: "Photo Frame", thumbnail: "5_sm.jpg", image: "5.jpg", badge: "New", artist: "Poonam Sonkar" },
    { id: 13, galleryId: 2, title: "Egg Shell-Smiley", thumbnail: "4_sm.jpg", image: "4.jpeg", badge: "New",artist:"Poonam Sonkar" },
    { id: 14, galleryId: 2, title: "Bottle art with woollen", thumbnail: "1_sm.jpg", image: "1.jpg", badge: "New",artist:"Poonam Sonkar" },
    { id: 15, galleryId: 2, title: "Egg Shell-Minions", thumbnail: "2_sm.jpg", image: "2.jpg", badge: "New",artist:"Poonam Sonkar" },
    { id: 16, galleryId: 2, title: "Bottle art with jute", thumbnail: "3_sm.jpg", image: "3.jpg", badge: "New",artist:"Poonam Sonkar" },

    { id: 17, galleryId: 3, title: "Rangoli 1", thumbnail: "1_sm.jpg", image: "1.jpg", badge: "New",artist:"Poonam Sonkar" },
    { id: 18, galleryId: 3, title: "Rangoli 2", thumbnail: "2_sm.jpg", image: "2.jpg", badge: "New",artist:"Poonam Sonkar" },
    { id: 19, galleryId: 3, title: "Rangoli 3", thumbnail: "3_sm.jpg", image: "3.jpeg", badge: "New",artist:"Poonam Sonkar" },
    { id: 20, galleryId: 3, title: "Rangoli 4", thumbnail: "4_sm.jpg", image: "4.jpg", badge: "New",artist:"Poonam Sonkar" },
    { id: 21, galleryId: 3, title: "Rangoli 5", thumbnail: "5_sm.jpg", image: "5.jpg", badge: "New",artist:"Poonam Sonkar" },
    { id: 22, galleryId: 3, title: "Rangoli 6", thumbnail: "6_sm.jpg", image: "6.jpg", badge: "New",artist:"Poonam Sonkar" },

    { id: 23, galleryId: 4, title: "Dragon Art", thumbnail: "1_sm.jpg", image: "1.jpeg", badge: "New",artist:"Poonam Sonkar" },
    { id: 24, galleryId: 4, title: "Lady Face Painting", thumbnail: "2_sm.jpg", image: "2.jpeg", badge: "New", artist: "Poonam Sonkar" },
    { id: 26, galleryId: 4, title: "Camel Wall Painting", thumbnail: "3.jpg", image: "3.jpg", badge: "New", artist: "Poonam Sonkar" },
];