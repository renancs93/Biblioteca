<template>
  <div>
    <Header></Header>
    <div class="autor-page">
      <h1>Autores</h1>
      <fieldset style="display: inline-block">
        <legend>Novo Cadastro</legend>
        <div>
          <label>Nome do Autor:</label>
          <input v-model="NovoAutor.nome" class="input" />
          <Button
            texto="Cadastrar"
            v-on:click.native.prevent="cadastrar()"
          ></Button>
        </div>
      </fieldset>
      <div class="error">
        {{ msgErro }}
      </div>
      <div class="lstVazia" v-show="autores.length == 0">
        AINDA NÃO EXISTEM AUTORES CADASTRADOS
      </div>
      <ul>
        <li
          class="list flex flex-space center"
          v-for="(autor, index) in autores"
          :key="autor.id"
        >
          <div>
            <label>ID: {{ autor.id }}</label> |
            <label>Nome: {{ autor.nome }}</label>
          </div>
          <div>
            <Button
              texto="Editar"
              v-on:click.native.prevent="showModalEdit(autor)"
            ></Button>
            <Button
              texto="Excluir"
              v-on:click.native.prevent="excluir(autor, index)"
            ></Button>
          </div>
        </li>
      </ul>

      <!-- Modal -->
      <modal v-if="showModal" @salvar="editar()" @cancelar="showModal = false">
        <h3 slot="header">Editar Autor</h3>
        <div slot="body">
          <Label>Nome:</Label>
          <input class="input" v-model="EditAutor.nome" />
        </div>
      </modal>
    </div>
  </div>
</template>

<script>
import Header from "@/components/Header.vue";
import Button from "@/components/Button.vue";
import Modal from "@/components/Modal.vue";

export default {
  name: "Autores",
  components: {
    Header,
    Button,
    Modal,
  },
  data() {
    return {
      msgErro: "",
      showModal: false,
      autores: [],
      NovoAutor: {
        nome: "",
        id: 0,
      },
      EditAutor: {
        nome: "",
        id: 0,
      },
    };
  },
  methods: {
    // FUNÇÃO DE BUSCA TODOS OS AUTORES
    carregarDados: async function () {
      this.msgErro = "";

      await fetch("http://localhost:50598/api/autor")
        .then((response) => response.json())
        .then((data) => {
          this.autores = data;
        });
    },
    // FUNÇÃO DE EXCLUSÃO
    excluir: async function (autor, index) {
      var config = {
        method: "DELETE",
        headers: new Headers({
          "Content-Type": "application/json; charset=UTF-8",
        }),
      };

      await fetch(`http://localhost:50598/api/autor/${autor.id}`, config)
        .then(async (response) => {
          if (response.ok) {
            this.autores.splice(index, 1);
          } else {
            const res = await response.json();
            if (res.message != null) this.msgErro = res.message;
          }
        })
        .catch((error) => {
          this.msgErro = error.message;
        });
    },
    // FUNÇÃO DE EDIÇÃO
    editar: async function () {
      if (this.EditAutor.nome.trim() != "") {
        var config = {
          method: "PUT",
          body: JSON.stringify(this.EditAutor),
          headers: new Headers({
            "Content-Type": "application/json; charset=UTF-8",
          }),
        };

        await fetch(
          `http://localhost:50598/api/autor/${this.EditAutor.id}`,
          config
        )
          .then((response) => response.json())
          .then(() => {
            this.carregarDados();
          })
          .catch((error) => {
            window.console.log(error);
            //this.msgErro = error.message;
          });
      }

      this.showModal = !this.showModal;
    },
    // MODAL DE EDIÇÃO
    showModalEdit: async function (autor) {
      this.EditAutor = Object.assign({}, autor);
      this.showModal = !this.showModal;
    },
    // FUNÇÃO DE CADASTRO
    cadastrar: async function () {
      this.msgErro = "";

      if (this.NovoAutor.nome.trim() == "") {
        this.msgErro = "Nome não pode ser vazio";
      } else {
        var config = {
          method: "POST",
          body: JSON.stringify(this.NovoAutor),
          headers: new Headers({
            "Content-Type": "application/json; charset=UTF-8",
          }),
        };

        await fetch("http://localhost:50598/api/autor", config)
          .then((response) => response.json())
          .then((data) => {
            this.autores.unshift(data);
          })
          .catch((error) => {
            this.msgErro = error.message;
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
