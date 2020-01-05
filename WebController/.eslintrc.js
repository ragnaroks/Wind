module.exports = {
    root: true,
    env: {
        browser: true,
        node: true
    },
    parserOptions: {
        parser: 'babel-eslint'
    },
    extends: [
        '@nuxtjs',
        'plugin:nuxt/recommended'
    ],
    // add your custom rules here
    rules: {
        'no-console': process.env.NODE_ENV === 'production' ? 'error' : 'off',
        'no-debugger': process.env.NODE_ENV === 'production' ? 'error' : 'off',
        'semi': ['error', 'always'],
        'key-spacing': ['error', {
            'beforeColon': false,
            'afterColon': false
        }],
        'spaced-comment': ['off'],
        'space-before-blocks': ['off'],
        'object-shorthand': ['error', 'never'],
        'space-before-function-paren': ['error', 'never'],
        'space-infix-ops':['off'],
        'indent': ['off'],
        'no-unused-vars':['warn'],
        'new-cap':['off'],
        'comma-spacing':['off'],
        'object-curly-spacing':['error','never'],
        'keyword-spacing':['off'],
        'block-spacing':['error','never'],
        'brace-style':['off'],
        'semi-spacing':['error',{'before':false,'after':false}],
        //vue
        'vue/html-indent':['off'],
        'vue/singleline-html-element-content-newline':['off'],
        'vue/max-attributes-per-line':['off']
    }
};
