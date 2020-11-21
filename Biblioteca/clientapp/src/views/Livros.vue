<template>
  <div>
    <Header></Header>
    <div class="livro-page">
      <h1>Livros</h1>
      <ul>
         <li v-for="livro in livros" :key="livro.id">
           {{ livro.nome }}
         </li>
       </ul>
    </div>
  </div>
</template>


<script>
import Header from "@/components/Header.vue";
export default {
  name: "Livros",
  components: {
    Header,
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
          window.console.log(data);
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
.livro-page{
  margin: 10px;
}
</style>