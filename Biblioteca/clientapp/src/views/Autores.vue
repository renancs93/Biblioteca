<template>
  <div>
    <Header></Header>
    <div class="autor-page">
      <h1>Autores</h1>
      <fieldset style="display: inline-block">
        <legend>Novo Cadastro</legend>
        <label>Nome do Autor:</label>
        <input v-model="NovoAutor.nome" class="input" />
        <Button
          texto="Cadastrar"
          v-on:click.native.prevent="cadastrar()"
        ></Button>
      </fieldset>
      <ul>
        <li
          class="list flex flex-space"
          v-for="autor in autores"
          :key="autor.id"
        >
          <div>
            <label>ID: {{ autor.id }}</label> |
            <label>Nome: {{ autor.nome }}</label>
          </div>
          <div>
            <Button texto="Editar"></Button>
            <Button texto="Excluir"></Button>
          </div>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import Header from "@/components/Header.vue";
import Button from "@/components/Button.vue";

export default {
  name: "Autores",
  components: {
    Header,
    Button,
  },
  data() {
    return {
      autores: [],
      NovoAutor: {
        Nome: "",
        Id: 0,
      },
    };
  },
  methods: {
    // http://localhost:50598/
    carregarDados: function () {
      fetch("http://localhost:50598/api/autor")
        .then((response) => response.json())
        .then((data) => {
          window.console.log(data);
          this.autores = data;
        });
    },
    cadastrar: async function () {
      if (this.NovoAutor.nome.trim() == "") {
        window.console.log("Vazio");
      } else {
        window.console.log("Cadastrar");
        //window.console.log(JSON.stringify(this.NovoAutor));

        var config = {
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
          },
          method: "POST",
          body: JSON.stringify(this.NovoAutor),
        };

        await fetch("http://localhost:50598/api/autor", config)
          .then((response) => response.json())
          .then((data) => {
            window.console.log(data);
            this.autores.push(data);
          });

        this.NovoAutor.nome = "";
      }
    },
  },
  created() {
    this.carregarDados();
  },
};
</script>

<style>
.autor-page {
  margin: 10px;
}
.list {
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
  transition: 0.2s;
  border-radius: 5px;
  padding: 15px;
  margin: 8px 0;
}

.list:hover {
  background-color: #ac7339;
}

.list:hover button {
  background-color: white;
  color: black;
}

.button {
  margin: 0 3px;
}

.input {
  height: 25px;
  margin: 0 5px;
  border-radius: 3px;
}
</style>
