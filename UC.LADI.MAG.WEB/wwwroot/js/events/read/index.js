$(document).ready(function () {
    var pages = [];
    var pairsOfPage = [];
    for (let i = 1; i <= 64; i++) {
        if (i == 1) {
            let objPage = {
                width: 800,
                height: 1200,
                uri: `/images/ilovepdf_pages-to-jpg/dong hoa xuan 2024_page-000${i}.jpg`,
            };

        } else {

        }
    }
    var options = {
        data: [
            [
                {
                    width: 800,
                    height: 1200,
                    uri: "/images/ilovepdf_pages-to-jpg/dong hoa xuan 2024_page-0001.jpg",
                },
            ],
            [
                {
                    width: 800,
                    height: 1200,
                    uri: "https://baodongnai.com.vn/file/e7837c02876411cd0187645a2551379f/022024/8a10a6948791f7260187925fc3b7000a_20240122084425_20240208202714.jpg?width=600&height=-&type=resize",
                },
                {
                    width: 800,
                    height: 1200,
                    uri: "https://baodongnai.com.vn/file/e7837c02876411cd0187645a2551379f/022024/8a10a6948791f7260187925fc3b7000a_20240122084425_20240208202714.jpg?width=600&height=-&type=resize",
                },
            ],
            [
                {
                    width: 800,
                    height: 1200,
                    uri: "https://baodongnai.com.vn/file/e7837c02876411cd0187645a2551379f/022024/8a10a6948791f7260187925fc3b7000a_20240122084425_20240208202714.jpg?width=600&height=-&type=resize",
                },
                {
                    width: 800,
                    height: 1200,
                    uri: "https://baodongnai.com.vn/file/e7837c02876411cd0187645a2551379f/022024/8a10a6948791f7260187925fc3b7000a_20240122084425_20240208202714.jpg?width=600&height=-&type=resize",
                },
            ],
            [
                {
                    width: 800,
                    height: 1200,
                    uri: "https://baodongnai.com.vn/file/e7837c02876411cd0187645a2551379f/022024/8a10a6948791f7260187925fc3b7000a_20240122084425_20240208202714.jpg?width=600&height=-&type=resize",
                },
                {
                    width: 800,
                    height: 1200,
                    uri: "https://baodongnai.com.vn/file/e7837c02876411cd0187645a2551379f/022024/8a10a6948791f7260187925fc3b7000a_20240122084425_20240208202714.jpg?width=600&height=-&type=resize",
                },
            ],
        ],

        ui: "full", // embed, full (responsive)
    };
    var br = new BookReader(options);

    // Let's go!
    br.init();
});
