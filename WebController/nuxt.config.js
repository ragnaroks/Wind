export default {
    mode:'spa',
    /*
    ** Headers of the page
    */
    head:{
        title:'controller for Wind2 v' + process.env.npm_package_version || 'controller for Wind2',
        meta:[
            {charset:'utf-8'},
            {name:'viewport', content:'width=device-width,initial-scale=1'},
            {hid:'description', name:'description', content:process.env.npm_package_description || ''},
            {name:'author', content:process.env.npm_package_author},
            {name:'version', content:process.env.npm_package_version}
        ],
        link:[
            {rel:'icon', type:'image/x-icon', href:'favicon.ico'}
        ]
    },
    /*
    ** Customize the progress-bar color
    */
    loading:{color:'#09f'},
    /*
    ** Global CSS
    */
    css:[
        //'view-design/dist/styles/iview.css',
        '@/assets/style.css'
    ],
    /*
    ** Plugins to load before mounting the App
    */
    plugins:[
        '@/plugins/view-design'
    ],
    /*
    ** Nuxt.js dev-modules
    */
    buildModules:[
        // Doc: https://github.com/nuxt-community/eslint-module
        '@nuxtjs/eslint-module',
        // Doc: https://github.com/nuxt-community/stylelint-module
        '@nuxtjs/stylelint-module'
    ],
    /*
    ** Nuxt.js modules
    */
    modules:[
        // Doc: https://axios.nuxtjs.org/usage
        '@nuxtjs/axios'
    ],
    /*
    ** Axios module configuration
    ** See https://axios.nuxtjs.org/options
    */
    axios:{
    },
    /*
    ** Build configuration
    */
    build:{
        /*
        ** You can extend webpack config here
        */
        extend:function(config, ctx) {
        }
    }
};
