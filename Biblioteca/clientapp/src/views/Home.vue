<template>
  <div>
    <Header></Header>
    <div class="home">
      <h1>Listagem de Livros</h1>
      <div class="lista-livros">
        <LivroItem :livro="item" v-for="item in livros" :key="item.id"></LivroItem>
      </div>
    </div>
  </div>
</template>

<script>
import Header from "@/components/Header.vue";
import LivroItem from "@/components/LivroItem.vue";

export default {
  name: "Home",
  components: {
    Header,
    LivroItem,
  },
  data() {
    return {
      livros: [],
    };
  },
  methods: {
    // http://localhost:50598/
    carregarDados: function () {
      fetch("http://localhost:50598/api/livro")
        .then((response) => response.json())
        .then((data) => {
          this.livros = data;
        });
    },
  },
  created() {
    this.carregarDados();
  },
};
</script>

<style>
.home {
  margin: 10px;
}
.lista-livros {
  display: flex;
}
</style>