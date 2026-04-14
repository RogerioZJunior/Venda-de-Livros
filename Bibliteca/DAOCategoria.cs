using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;//Importando os comandos de conexão com o banco


namespace Bibliteca
{
    class DAOCategoria
    {
        public MySqlConnection conexao;
        public string dados;
        public string comando;
        public int[] codigo;
        public string[] descricao;
        public int i;
        public int contar;
        public string msg;
        public DAOCategoria()
        {
            //Conexão com o banco de dados
            this.conexao = new MySqlConnection("server=localhost;DataBase=VendaDeLivros;Uid=root;Password=;Convert Zero DateTime=True");
            try
            {
                this.conexao.Open();//Abrir a conexão
                Console.WriteLine("Conectado com sucesso!");
            }
            catch (Exception erro)
            {
                Console.WriteLine($"Algo deu errado!\n\n {erro}");
                this.conexao.Close();//Fechar a conexão com o BD
            }//fim do try_catch
        }//fim do construtor


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
            this.codigo = new int[100];
            this.descricao = new string[100];

            //Preencher os vetores com valores padrões
            for (i = 0; i < 100; i++)
            {
                this.codigo[i] = 0;
                this.descricao[i] = "";
               
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
                this.codigo[i] = Convert.ToInt32(leitura["codigo"]);
                this.descricao[i] = leitura["descricao"] + "";
                i++;
                this.contar++;//Informar quantos dados tem no banco
            }//fim do while

            leitura.Close();//Encerrando o processo de busca

        }//fim da classe





    }//fim do projeto





