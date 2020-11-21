<template>
  <div class="card">
    <div class="conteudo">
      <div class="titulo">
        {{ livro.nome }}
      </div>
      <div class="autor">
        Autor:
        {{ livro.autor != null ? livro.autor.nome : "Autor Desconhecido" }}
      </div>
      <div class="estoque">Estoque: {{ livro.qtdEstoque }}</div>

      <button class="button">COMPRAR</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "LivroItem",
  props: {
    livro: Object,
  },
};
</script>

<style>
.card {
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
  transition: 0.2s;
  border-radius: 5px;
  padding: 10px;
  margin: 5px;
  width: 150px;
  height: 200px;
}
.card:hover {
  box-shadow: 0 8px 16px 0 rgba(0, 0, 0, 0.2);
}
.container {
  padding: 2px 16px;
}

.conteudo {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
  height: 100%;
  text-align: center;
}

.titulo {
  font-weight: bold;
  font-size: medium;
}

.autor {
  font-style: italic;
}

.estoque {
  font-size: small;
}
.button{
    background-color:#ac7339;
    padding: 10px 15px;
    border-radius: 5px;
    font-weight: bold;
    color: white;
}

.button:hover{
    filter: drop-shadow(1px 1px 3px black);
    cursor: pointer;
}

</style>