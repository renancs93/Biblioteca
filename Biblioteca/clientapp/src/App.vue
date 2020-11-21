<template>
  <div id="app">
    <router-view />
  </div>
</template>

<script>
export default {
  name: "App",
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  /* text-align: center; */
}

body,
ul,
li {
  margin: 0;
  padding: 0;
  background-color: #f0f0f0;
  list-style-type: none;
}

.flex {
  display: flex;
}

.flex-space {
  justify-content: space-between;
}

.error{
  color: red;
  font-size: small;
}

.center {
  align-items: center
}

</style>
