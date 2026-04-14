using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;//Importando os comandos de conexão com o banco

namespace Bibliteca
{
    class DAOLivro
    {
        public MySqlConnection conexao;//Criando a variável que representa o banco
        public string dados;
        public string comando;
        public int[] codigo;
        public string[] titulo;
        public string[] preco;
        public string[] quantidade;
        public string[] autor;
        public string[] editora;
        public string[] categoria_codigo;
        public string[] categoria;
        public int i;
        public int contar;
        public string msg;
        public DAOLivro()
        {
            //Conexão com o banco de dados
            this.conexao = new MySqlConnection("server=localhost;DataBase=VendaDeLivros;Uid=root;Password=;Convert Zero DateTime=True");
            try
            {
                this.conexao.Open();//Abrir a conexão
                Console.WriteLine("Conectado com sucesso!");
            }
            catch(Exception erro)
            {
                Console.WriteLine($"Algo deu errado!\n\n {erro}");
                this.conexao.Close();//Fechar a conexão com o BD
            }//fim do try_catch
        }//fim do construtor

        //Inserir o dado no banco
        public void Inserir(string titulo, string autor, string quantidade, string preco, string editora, string categoria_codigo)
        {
            try
            {
                this.dados = $"('','{titulo}','{autor}','{quantidade}','{preco}','{editora}','{categoria_codigo}')";
                this.comando = $"Insert into livro(codigo, titulo, autor, quantidade, preco, editora, categoria_codigo) values{this.dados}";
                //Inserir o comando no banco
                MySqlCommand sql = new MySqlCommand(this.comando, this.conexao);
                string resultado = "" + sql.ExecuteNonQuery();//Executo o comando
                Console.WriteLine($"Inserido com sucesso! \n\n{resultado}");
            }
            catch(Exception erro)
            {
                Console.WriteLine($"Algo deu errado\n\n {erro}");
            }//fim do catch
        }//fim do método

        //Preencher Vetor --> Coletar os dados do banco e preenhcer o vetor
        public void PreencherVetor()
        {
            string query = "select * from livro";//Buscando todos os dados da tabela livro
            //Instanciar os vetores
            this.codigo           = new int[100];
            this.titulo           = new string[100];
            this.autor            = new string[100];
            this.quantidade       = new string[100];
            this.preco            = new string[100];
            this.editora          = new string[100];
            this.categoria_codigo = new string[100];

            //Preencher os vetores com valores padrões
            for (i = 0; i < 100; i++)
            {
                this.codigo[i]           = 0;
                this.titulo[i]           = "";
                this.autor[i]            = "";
                this.quantidade[i]       = "";
                this.preco[i]            = "";
                this.editora[i]          = "";
                this.categoria_codigo[i] = "";

            }//fim do for

            //Executar o comando do SQL
            MySqlCommand coletar = new MySqlCommand(query, this.conexao);

            //Leitura do dado no banco
            MySqlDataReader leitura = coletar.ExecuteReader();//Percorre o banco e traz os dados

            //Zerar o contador
            i = 0;
            this.contar = 0;
            while (leitura.Read())
            {
                this.codigo[i]       = Convert.ToInt32(leitura["codigo"]);
                this.titulo[i]       = leitura["titulo"] + "";
                this.autor[i]        = leitura["autor"] + "";
                this.quantidade[i]   = leitura["quantidade"] + "";
                this.preco[i]        = leitura["preco"] + "";
                this.editora[i]      = leitura["editora"] + "";
                this.categoria_codigo[i] = leitura["categoria_codigo"] + "";
                i++;
                this.contar++;//Informar quantos dados tem no banco
            }//fim do while

            leitura.Close();//Encerrando o processo de busca
        }//fim do método

        public string ConsultarTudo()
        {
            PreencherVetor();//Preencher todos os dados do vetor
            this.msg = "";
            for(i = 0; i < this.contar; i++)
            {
                this.msg += $"\nCódigo:                  {this.codigo[i]} "        +
                            $"\nNome:                    {this.titulo[i]} "        +
                            $"\nAutor:                   {this.autor[i]}"          +
                            $"\nQuantidade:              {this.quantidade[i]}"     +
                            $"\nPreço:                   {this.preco[i]}"          +
                            $"\nEditora:                 {this.editora[i]}\n\n"    +
                            $"\nCodigo de Categoria:     {this.categoria_codigo[i]}\n\n";
            }
            return this.msg;
        }//fim do método consultarTudo

        public string ConsultarPorCodigo(int codigo)
        {
            PreencherVetor();//Preencher todos os dados do vetor
            this.msg = "";
            for (i = 0; i < this.contar; i++)
            {
                if (this.codigo[i] == codigo)
                {
                   
                this.msg += $"\nCódigo:                  {this.codigo[i]} "        +
                            $"\nNome:                    {this.titulo[i]} "        +
                            $"\nAutor:                   {this.autor[i]}"          +
                            $"\nQuantidade:              {this.quantidade[i]}"     +
                            $"\nPreço:                   {this.preco[i]}"          +
                            $"\nEditora:                 {this.editora[i]}\n\n"   + 
                            $"\nCodigo de Categoria:     {this.categoria_codigo[i]}\n\n";

                    return this.msg;
                }//fim do if              
            }//fim do for
            return "Código informado não existe!";
        }//fim do método consultarTudo

        public string Atualizar(int codigo, string campo, string novoDado)
        {
            try
            {
                string query = $"update autor set {campo} = '{novoDado}' where codigo = '{codigo}'";
                //Executar o comando
                MySqlCommand sql = new MySqlCommand(query, this.conexao);
                string resultado = "" + sql.ExecuteNonQuery();//Comando de inserção no banco
                return $"Atualizado com sucesso\n\n {resultado}";
            }
            catch(Exception erro)
            {
                return $"Algo deu errado\n\n {erro}";
            }
        }//fim do atualizar

        public string Deletar(int codigo)
        {
            try
            {
                string query = $"delete from autor where codigo = '{codigo}'";
                //Executar o comando
                MySqlCommand sql = new MySqlCommand(query, this.conexao);
                string resultado = "" + sql.ExecuteNonQuery();//Comando de inserção no banco
                return $"Deletado com sucesso\n\n {resultado}";
            }
            catch (Exception erro)
            {
                return $"Algo deu errado\n\n {erro}";
            }
        }//fim do deletar



        //Inserir categoria o dado no banco 
        public void Inserir_categoria(int codigo, string descricao)
        {
            try
            {
                this.dados = $"('','{codigo}','{descricao}')";
                this.comando = $"Insert into categoria(codigo, descricao) values{this.dados}";
                //Inserir o comando no banco
                MySqlCommand sql = new MySqlCommand(this.comando, this.conexao);
                string resultado = "" + sql.ExecuteNonQuery();//Executo o comando
                Console.WriteLine($"Inserido com sucesso! \n\n{resultado}");
            }
            catch (Exception erro)
            {
                Console.WriteLine($"Algo deu errado\n\n {erro}");
            }//fim do catch
        }//fim do método

        public void PreencherCategoriaVetor()
        {
            string query = "select * from categoria";//Buscando todos os dados da tabela categoria
            //Instanciar os vetores
            this.codigo    = new int[100];
            this.decricao  = new string[100];       









        }//fim da classe
    }//fim do projeto
