import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'

export default {
    // eslint-disable-next-line no-unused-vars
    install(Vue) {
        const notifyHub = new Vue();
        Vue.prototype.$notificationHub = notifyHub

        const connection = new HubConnectionBuilder()
            .withUrl('https://localhost:5003/notification-hub')
            .configureLogging(LogLevel.Information)
            .build()

        let startedPromise = null
        function start() {
            startedPromise = connection.start({ withCredentials: false }).catch(err => {
                console.error('Failed to connect with hub', err)
                return new Promise((resolve, reject) =>
                    setTimeout(() => start().then(resolve).catch(reject), 5000))
            })
            return startedPromise
        }
        connection.onclose(() => start())

        connection.on('test', t => notifyHub.$emit('test', t))
        notifyHub.test = () => {
            if(!startedPromise) return

            startedPromise.then(() => {
                connection.invoke('test')
            })
        }

        start()
    }
}