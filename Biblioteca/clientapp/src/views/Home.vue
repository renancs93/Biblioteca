<template>
  <div>
    <Header></Header>
    <div class="home">
      <h1>Listagem de Livros</h1>
      <div v-for="item in livros" :key="item.id">
          {{ item.nome }}
      </div>
    </div>
  </div>
</template>

<script>
import Header from "@/components/Header.vue";

export default {
  name: "Home",
  components: {
    Header,
  },
  data() {
    return {
      livros: [],
    };
  },

  created() {
    this.livros = [
        {
            nome: "Teste1"
        },
        {
            nome: "Teste1"
        },
        {
            nome: "Teste1"
        },
    ]
  },
};
</script>
