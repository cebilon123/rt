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
    this.$notificationHub.$on("Notification", (t) => {
      switch (t.type) {
        case 0:
          this.$notify({
            group: "notify",
            title: "Information",
            text: t.message,
          });
          break;
        case 2:
          this.$notify({
            type: "error",
            group: "notify",
            title: "Error",
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
