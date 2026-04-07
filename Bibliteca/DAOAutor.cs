using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;//Importando os comandos de conexão com o banco

namespace Bibliteca
{
    class DAOAutor
    {
        public MySqlConnection conexao;//Criando a variável que representa o banco
        public string dados;
        public string comando;
        public int[] codigo;
        public string[] nome;
        public string[] genero;
        public string[] endereco;
        public int i;
        public int contar;
        public string msg;
        public DAOAutor()
        {
            //Conexão com o banco de dados
            this.conexao = new MySqlConnection("server=localhost;DataBase=registro;Uid=root;Password=;Convert Zero DateTime=True");
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
        public void Inserir(string nome, string genero, string endereco)
        {
            try
            {
                this.dados = $"('','{nome}','{genero}','{endereco}')";
                this.comando = $"Insert into autor(codigo, nome, genero, endereco) values{this.dados}";
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
            string query = "select * from autor";//Buscando todos os dados da tabela autor
            //Instanciar os vetores
            this.codigo   = new int[100];
            this.nome     = new string[100];
            this.genero   = new string[100];
            this.endereco = new string[100];

            //Preencher os vetores com valores padrões
            for(i = 0; i < 100; i++)
            {
                this.codigo[i]   = 0;
                this.nome[i]     = "";
                this.genero[i]   = "";
                this.endereco[i] = "";
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
                this.codigo[i]   = Convert.ToInt32(leitura["codigo"]);
                this.nome[i]     = leitura["nome"] + "";
                this.genero[i]   = leitura["genero"] + "";
                this.endereco[i] = leitura["endereco"] + "";
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
                this.msg += $"\nCódigo:   {this.codigo[i]} " +
                            $"\nNome:     {this.nome[i]} "   +
                            $"\nGênero:   {this.genero[i]}"  +
                            $"\nEndereço: {this.endereco[i]}\n\n";
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
                    this.msg = $"\nCódigo:   {this.codigo[i]} " +
                                $"\nNome:     {this.nome[i]} " +
                                $"\nGênero:   {this.genero[i]}" +
                                $"\nEndereço: {this.endereco[i]}\n\n";
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




    }//fim da classe
}//fim do projeto
