export const dev = window.location.origin.includes('localhost')
export const baseURL = dev ? 'https://localhost:5001' : ''
export const useSockets = false
export const domain = 'the-begining.us.auth0.com'
export const audience = 'https://codeworkclass.in'
export const clientId = 'cnZwVjQ1nHaUiYClteFn1CVFHpTdIwEB'