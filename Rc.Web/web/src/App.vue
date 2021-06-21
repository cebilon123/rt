<template>
  <div id="app">
    <img alt="Vue logo" src="./assets/logo.png" />
    <HelloWorld msg="Welcome to Your Vue.js App" />
    <notifications group="notify" />
  </div>
</template>

<script>
import HelloWorld from "./components/HelloWorld.vue";

export default {
  name: "App",
  components: {
    HelloWorld,
  },

  mounted() {
    this.$notify({
      group: "notify",
      title: "Important message",
      text: "Hello user! This is a notification!",
    });

    this.$notificationHub.$on("Notification", (t) => {
      switch (t.type) {
        case 0:
          this.$notify({
            group: "notify",
            title: "Information",
            text: t.message,
          });
          break;

        default:
          break;
      }
    });
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
