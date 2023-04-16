namespace APICatalago.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string? Nome { get; set; }
        public string? MyProperty { get; set; }
        public decimal Preco { get; set; }
        public string? ImegmUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
       
    }
}
