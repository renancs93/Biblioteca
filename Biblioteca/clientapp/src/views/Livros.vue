<template>
  <div>
    <Header></Header>
    <div class="livro-page">
      <h1>Livros</h1>
      <fieldset style="display: inline-block">
        <legend>Novo Cadastro</legend>
        <div>
          <label>Nome do Livro:</label>
          <input v-model="NovoLivro.nome" class="input" />
          <label for="ddlAutores">Autor:</label>
          <select
            v-model="NovoLivro.idAutor"
            class="select"
            id="ddlAutores"
            name="autores"
          >
            <option v-for="autor in autores" :key="autor.id" :value="autor.id">
              {{ autor.nome }}
            </option>
          </select>
          <label>Qtde. Estoque:</label>
          <input
            type="number"
            min="0"
            v-model.number="NovoLivro.qtdEstoque"
            class="input"
          />
          <Button
            texto="Cadastrar"
            v-on:click.native.prevent="cadastrar()"
          ></Button>
        </div>
      </fieldset>
      <div class="error">
        {{ msgErro }}
      </div>
      <div class="lstVazia" v-show="livros.length == 0">
        AINDA NÃO EXISTEM LIVROS CADASTRADOS
      </div>
      <ul>
        <li
          class="list flex flex-space center"
          v-for="(livro, index) in livros"
          :key="livro.id"
        >
          <div>
            <label>ID: {{ livro.id }}</label> |
            <label>Livro: {{ livro.nome }}</label> |
            <label
              >Autor:
              {{
                livro.autor != null ? livro.autor.nome : "Autor Desconhecido"
              }}</label
            >
            |
            <label>Estoque: {{ livro.qtdEstoque }}</label>
          </div>
          <div>
            <Button
              texto="Editar"
              v-on:click.native.prevent="showModalEdit(livro)"
            ></Button>
            <Button
              texto="Excluir"
              v-on:click.native.prevent="excluir(livro, index)"
            ></Button>
          </div>
        </li>
      </ul>

      <!-- Modal -->
      <modal v-if="showModal" @salvar="editar()" @cancelar="showModal = false">
        <h3 slot="header">Editar Livro</h3>
        <div class="flex" slot="body">
          <div class="labels">
            <Label>Nome:</Label>
            <label for="ddlAutores">Autor:</label>
            <Label>Qtde. Estoque:</Label>
          </div>
          <div class="campos">
            <input class="input" v-model="EditLivro.nome" />
            <select
              v-model="EditLivro.idAutor"
              class="select"
              name="autoresEdit"
            >
              <option
                v-for="autor in autores"
                :key="autor.id"
                :value="autor.id"
              >
                {{ autor.nome }}
              </option>
            </select>
            <input
              type="number"
              min="0"
              v-model.number="EditLivro.qtdEstoque"
              class="input"
            />
          </div>
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
  name: "Livros",
  components: {
    Header,
    Button,
    Modal,
  },
  data() {
    return {
      msgErro: "",
      showModal: false,
      livros: [],
      autores: [{ id: 0, nome: "Autor Desconhecido" }],
      NovoLivro: {
        nome: "",
        qtdEstoque: 0,
        idAutor: 0,
      },
      EditLivro: {
        nome: "",
        qtdEstoque: 0,
        idAutor: 0,
        id: 0,
      },
    };
  },
  methods: {
    carregarDados: function () {
      fetch(`${this.$http}/livro`)
        .then((response) => response.json())
        .then((data) => {
          this.livros = data;
        })
        .catch((error) => {
          if (error == "TypeError: Failed to fetch")
            this.msgErro = "Erro ao consultar dados ao servidor";
        });
    },
    carregarDadosAutores: async function () {
      this.msgErro = "";

      await fetch(`${this.$http}/autor`)
        .then((response) => response.json())
        .then((data) => {
          data.map((item) => {
            this.autores.push(item);
          });
        });
    },
    // MODAL DE EDIÇÃO
    showModalEdit: async function (livro) {
      this.EditLivro = Object.assign({}, livro);
      // set autor
      this.EditLivro.idAutor = livro.autor != null ? livro.autor.id : 0;
      this.showModal = !this.showModal;
    },
    // FUNÇÃO DE CADASTRO
    cadastrar: async function () {
      this.msgErro = "";

      if (this.NovoLivro.nome.trim() == "") {
        this.msgErro = "Nome do livro não pode ser vazio!";
      } else if (this.NovoLivro.qtdEstoque < 0) {
        this.msgErro = "Quantidade do estoque não pode ser negativa!";
      } else {
        var config = {
          method: "POST",
          body: JSON.stringify(this.NovoLivro),
          headers: new Headers({
            "Content-Type": "application/json; charset=UTF-8",
          }),
        };

        await fetch(`${this.$http}/livro`, config)
          .then(async (response) => await response.json())
          .then((data) => {
            this.livros.unshift(data);

            this.NovoLivro.nome = "";
            this.NovoLivro.qtdEstoque = 0;
          })
          .catch((error) => {
            this.msgErro = error.message;
          });
      }
    },
    // FUNÇÃO DE EDIÇÃO
    editar: async function () {
      if (this.EditLivro.nome.trim() != "") {
        var config = {
          method: "PUT",
          body: JSON.stringify(this.EditLivro),
          headers: new Headers({
            "Content-Type": "application/json; charset=UTF-8",
          }),
        };

        await fetch(`${this.$http}/livro/${this.EditLivro.id}`, config)
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
    // FUNÇÃO DE EXCLUSÃO
    excluir: async function (livro, index) {
      var config = {
        method: "DELETE",
        headers: new Headers({
          "Content-Type": "application/json; charset=UTF-8",
        }),
      };

      await fetch(`${this.$http}/livro/${livro.id}`, config)
        .then(async (response) => {
          if (response.ok) {
            this.livros.splice(index, 1);
          } else {
            const res = await response.json();
            if (res.message != null) this.msgErro = res.message;
          }
        })
        .catch((error) => {
          this.msgErro = error.message;
        });
    },
  },
  created() {
    this.carregarDados();
    this.carregarDadosAutores();
  },
};
</script>

<style>
.livro-page {
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

.input,
.select {
  height: 25px;
  margin: 0 5px;
  border-radius: 3px;
}

.select {
  min-width: 177px;
  height: 31px;
  margin: 0 5px;
  padding: 0 5px;
}

.labels {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.labels > label {
  height: 25px;
}

.campos {
  display: flex;
  flex-direction: column;
}
</style>