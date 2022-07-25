using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jogo_Zumbi
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, goUp, goDown, gameOver;
        // goUp: Este booleano será usado para o jogador subir na tela
        // goDown: Este booleano será usado para o jogador descer a tela 
        // goLeft: Este booleano será usado para o jogador ir para a esquerda na tela
        // goRight: Este booleano será usado para o jogador ir para a direita na tela
        string facing = "up";// Esta string é chamada de virada e será usada para guiar as balas
        int playerHealth = 100; // Esta variável dupla é chamada de saúde do jogador
        int speed = 10; // Este número inteiro é para a velocidade do jogador
        int ammo = 10; // Este inteiro conterá o número de munição que o jogador tem no início do jogo
        int zombieSpeed = 3; // Este inteiro irá manter a velocidade que os zumbis se movem no jogo

        Random randNum = new Random();
        // Esta é uma instância da classe random vamos usar isso para criar um número aleatório para este jogo
        int score; // Esse número inteiro conterá a pontuação que o jogador alcançou no jogo

        // Fim da lista de variáveis

        List<PictureBox> zombiesList = new List<PictureBox>();



        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {

            if (playerHealth > 1) // Se a saúde do jogador for maior que 1
            {
                healthBar.Value = playerHealth; // Atribua a barra de progresso ao inteiro de saúde do jogador

            }
            else
            {
                gameOver = true; // Mudar o jogo para verdadeiro
                player.Image = Properties.Resources.dead; // Mostrar a imagem do jogador morto
                GameTimer.Stop(); // Parar o temporizador do game
            }

            txtAmmo.Text = "Ammo: " + ammo; // Mostre a quantidade de munição na etiqueta 
            txtScore.Text = "Kills: " + score; // Mostre a a quantidade de munição na etiqueta

            
            if (goLeft == true && player.Left > 0)
            {
                player.Left -= speed;
            }
            if (goRight == true && player.Left + player.Width < this.ClientSize.Width)
            {
                player.Left += speed;
            }
            if (goUp == true && player.Top > 45)
            {
                player.Top -= speed;
            }
            if (goDown == true && player.Top + player.Height < this.ClientSize.Height)
            {
                player.Top += speed;

            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "ammo")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        ammo += 5;


                    }
                }

                if (x is PictureBox && (string)x.Tag == "zombie")
                {

                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        playerHealth -= 1;
                    }




                    if (x.Left > player.Left)
                    {
                        x.Left -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zleft;

                    }
                    if (x.Left < player.Left)
                    {
                        x.Left += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zright;

                    }
                    if (x.Top > player.Top)
                    {
                        x.Top -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zup;

                    }
                    if (x.Top < player.Top)
                    {
                        x.Top += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zdown;

                    }




                }

                foreach (Control j in this.Controls)
                {
                    if (j is PictureBox && (string)j.Tag == "bullet" && x is PictureBox && (string)x.Tag == "zombie")
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;

                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            zombiesList.Remove(((PictureBox)x));
                            MakeZombies();


                        }

                    }
                }




            }


        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {

            // Se o fim do jogo for verdadeiro, não faça nada neste evento
            if (gameOver == true)
            {
                return;

            }
            // Se a tecla esquerda for pressionada, faça o seguinte
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true; // Alterar vá para a esquerda para true
                facing = "left"; // Mudar para a esquerda
                player.Image = Properties.Resources.left; // Mude a imagem do player para a imagem esquerda
            }

            // Se a tecla direita for pressionada, faça o seguinte
            if (e.KeyCode == Keys.Right)
            {
                goRight = true; // Alterar vá para a direita para true 
                facing = "right"; // Mudar para a direita
                player.Image = Properties.Resources.right; // Mude a imagem do player para a imagem direita
            }

            // Se a tecla para cima for pressionada, faça o seguinte
            if (e.KeyCode == Keys.Up)
            {
                goUp = true; // Alterar vá para cima para true
                facing = "up"; // Mudar para cima
                player.Image = Properties.Resources.up; // Mudar a imagem do player pra imagem para cima
            }
            // Se a tecla para baixo for pressionada, faça o seguinte
            if (e.KeyCode == Keys.Down)
            {
                goDown = true; // Alterar vá para baixo para true 
                facing = "down"; // Mudar para baixo
                player.Image = Properties.Resources.down; // Mudar a imagem do player pra imagem para baixo
            }



        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            // Abaixo está a seleção de tecla para cima para a tecla esquerda
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false; // Trocando o booleano go left para false

            }
            // Abaixo está a seleção de tecla para cima para a tecla direita
            if (e.KeyCode == Keys.Right)
            {
                goRight = false; // Trocando o booleano go right para false

            }
            // Abaixo está a seleção de tecla para cima para tecla para cima
            if (e.KeyCode == Keys.Up)
            {
                goUp = false; // Trocando o booleano go up para false

            }
            // Abaixo está a seleção de tecla para cima para a tecla para baixo
            if (e.KeyCode == Keys.Down)
            {
                goDown = false; // Trocando o booleano para false

            }

            // Abaixo está a seleção de tecla para cima para a tecla de espaço
            if (e.KeyCode == Keys.Space && ammo > 0 && gameOver == false)
            {
                ammo--; // Reduza a munição em 1 do número total
                ShootBullet(facing); // Invoque a função ShootBullet com a string oposta dentro dela
                // Enfrentando irá transferir para cima, para baixo, para a esquerda ou para a direita para a função e que irá disparar a bala dessa forma

                if (ammo < 1) // Se a munição for menor que 1
                {
                    DropAmmo(); // Invocar a função de soltar munição

                }
            }

            if (e.KeyCode == Keys.Enter && gameOver == true) // Se a tecla enter for pressionada, game over é verdadeiro
            {
                RestartGame(); // Chama a função reiniciar o jogo 
            }

        }

        private void ShootBullet(string direction)
        {
            // Função que faz as novas balas no jogo

            Bullet shootBullet = new Bullet(); // Crie uma nova instância da classe bullet
            shootBullet.direction = direction; // Atribuir a direção para a bala
            shootBullet.bulletLeft = player.Left + (player.Width / 2); // Coloque a bala na metade esquerda do jogador
            shootBullet.bulletTop = player.Top + (player.Height / 2); // Coloque a bala na metade superior do jogador
            shootBullet.MakeBullet(this); // Execute a função MakeBullet da classe bullet

        }

        private void MakeZombies()
        {
            // Quando esta função for chamada ela fará zumbis no jogo


            PictureBox zombie = new PictureBox(); // Crie uma nova caixa de imagem chamada zumbi 
            zombie.Tag = "zombie"; // Adicione uma tag a ele chamada zumbi
            zombie.Image = Properties.Resources.zdown; // A imagem padrão para o zumbi é zdown
            zombie.Left = randNum.Next(0, 900); // Gerar um número entre 0 e 900 e atribuir isso aos novos zumbis deixados
            zombie.Top = randNum.Next(0, 800); // Gere um número entre  0 e 800 e atribua isso aos novos zumbis top
            zombie.SizeMode = PictureBoxSizeMode.AutoSize; // Definir tamanho automático para a nova caixa de imagem
            zombiesList.Add(zombie);
            this.Controls.Add(zombie); // Adicione a caixa de imagem à tela
            player.BringToFront(); // Trazer o jogador para a frente



        }

        private void DropAmmo()
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo_Image;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = randNum.Next(10, this.ClientSize.Width - ammo.Width);
            ammo.Top = randNum.Next(60, this.ClientSize.Height - ammo.Height);
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);

            ammo.BringToFront();
            player.BringToFront();

        }

        private void RestartGame()
        {

            player.Image = Properties.Resources.up;

            foreach (PictureBox i in zombiesList)
            {
                this.Controls.Remove(i);

            }

            zombiesList.Clear();

            for (int i = 0; i < 3; i++)
            {
                MakeZombies();
            }

            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;
            gameOver = false;

            playerHealth = 100;
            score = 0;
            ammo = 10;

            GameTimer.Start();


        }
    }
}
