<template>
  <div>
    <Header></Header>
    <div class="home">
      <h1>Listagem de Livros</h1>
      <div class="lstVazia" v-show="livros.length == 0">
        AINDA NÃO EXISTEM LIVROS CADASTRADOS
      </div>
      <div class="mensagem">
        {{ msg }}
      </div>
      <div class="error">
        {{ msgErro }}
      </div>
      <div class="lista-livros">
        <LivroItem
          :livro="item"
          v-for="item in livros"
          :key="item.id"
          @comprar="comprar(item)"
        ></LivroItem>
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
      msgErro: "",
      msg: "",
    };
  },
  methods: {
    // http://localhost:50598/
    carregarDados: function () {
      this.msgErro = "";

      fetch(`${this.$http}/livro`)
        .then((response) => response.json())
        .then((data) => {
          this.livros = data;
        });
    },
    comprar: async function (livro) {
      this.msgErro = "";
      this.msg = "";

      var config = {
        method: "POST",
        body: JSON.stringify(livro),
        headers: new Headers({
          "Content-Type": "application/json; charset=UTF-8",
        }),
      };

      await fetch(`${this.$http}/livro/comprar`, config)
        .then(async (response) => {
          if (response.ok) {
            this.carregarDados();
            this.msg = "Compra realizada com sucesso!";
          } else {
            if (response.status == 403) {
              var ret = await response.json();
              this.msgErro = ret.message;
            } else
              this.msgErro = "Ocorreu um erro inesperado ao realizar comprar!";
          }
        })
        .catch((error) => {
          this.msgErro = error.message;
          window.console.log("ERRO", error);
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

.mensagem {
  text-align: center;
  margin: 10px 0;
}
</style>