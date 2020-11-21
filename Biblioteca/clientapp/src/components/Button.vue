<template>
  <button class="button">{{ texto }}</button>
</template>

<script>
export default {
  name: "Button",
  props: {
    texto: {
      type: String,
      default: "Botão"
    },
  },
};
</script>

<style>
.button {
  background-color: #ac7339;
  padding: 8px 15px;
  border-radius: 5px;
  font-weight: bold;
  color: white;
}

.button:hover {
  filter: drop-shadow(1px 1px 3px black);
  cursor: pointer;
}
</style>