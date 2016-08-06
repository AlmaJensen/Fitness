using FitnessGame.DataModels;
using FitnessGame.Interfaces;
using FitnessGame.Services;
using FreshMvvm;
using PropertyChanged;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FitnessGame.PageModels
{
    [ImplementPropertyChanged]
    public class HomePageModel : NavCommands
    {
        private Realm _realmdb;
        public PlayerInfo PlayerData { get; set; }
        public bool Bar1Visible { get; set; } = false;
        public bool Bar2Visible { get; set; } = false;
        public bool Bar3Visible { get; set; } = false;
        public bool Bar4Visible { get; set; } = false;
        public bool Bar5Visible { get; set; } = false;
        public bool Bar6Visible { get; set; } = false;
        public bool Bar7Visible { get; set; } = false;
        public bool Bar8Visible { get; set; } = false;
        public bool Bar9Visible { get; set; } = false;
        public bool Bar10Visible { get; set; } = false;
        public bool Bar11Visible { get; set; } = false;
        public bool Bar12Visible { get; set; } = false;
        public bool Bar13Visible { get; set; } = false;
        public bool Bar14Visible { get; set; } = false;
        public bool Bar15Visible { get; set; } = false;
        public bool Bar16Visible { get; set; } = false;
        public bool Bar17Visible { get; set; } = false;
        public bool Bar18Visible { get; set; } = false;
        public bool Bar19Visible { get; set; } = false;
        public bool Bar20Visible { get; set; } = false;

        public int HeroPosition { get; set; }

        public ImageSource Tile1 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile2 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile3 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile4 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile5 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile6 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile7 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile8 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile9 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile10 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile11 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile12 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile13 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile14 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile15 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile16 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile17 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile18 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile19 { get; set; } = ImageSource.FromFile("whiteTile.png");
        public ImageSource Tile20 { get; set; } = ImageSource.FromFile("whiteTile.png");

        public override void Init(object initData)
        {
            base.Init(initData);
            _realmdb = Realm.GetInstance();
            var playerLoad = _realmdb.All<PlayerInfo>();

            PlayerData = playerLoad.First();

            _realmdb.RealmChanged += _realmdb_RealmChanged;

            StartStepCounter();

            foreach (var t in PlayerData.FlippedTiles)
                UpdateTile(t);
        }

        private void _realmdb_RealmChanged(object sender, EventArgs e)
        {
            var playerLoad = _realmdb.All<PlayerInfo>();
            PlayerData = playerLoad.First();
            if (PlayerData.Steps.First().UnprocessedStepCount > 0)
            {
                ProcessSteps();
            }
        }

        private void ProcessSteps()
        {
            var pastDays = PlayerData.Steps.Where(x => x.DateOfSteps != DateTime.Today);
            if (pastDays.Count() > 0)
            {
                var f = new Frame();
                foreach (var s in pastDays)
                    _realmdb.Write(() =>
                    {
                        var p = _realmdb.All<PlayerInfo>().First();
                        p.MoneyEarned += s.UnprocessedStepCount;
                        p.Steps.Remove(s);
                    });
            }
            try
            {
                var playerInfo = _realmdb.All<PlayerInfo>().First();
                var todaysSteps = playerInfo.Steps.Where(x => x.DateOfSteps == DateTime.Today).First();
                if (todaysSteps.UnprocessedStepCount > 0)
                {
                    var tempCount = todaysSteps.UnprocessedStepCount;
                    _realmdb.Write(() =>
                    {
                        todaysSteps.UnprocessedStepCount = 0;
                        playerInfo.MoneyEarned += tempCount;
                        todaysSteps.ProcessedStepCount += tempCount;
                        todaysSteps.AvailableSearches = CalculateAvailableSearches(todaysSteps.ProcessedStepCount);
                        playerInfo.SearchesAvailable = todaysSteps.AvailableSearches - playerInfo.SearchesCompleted;
                    });
                    UpdateBarVisibility(todaysSteps.AvailableSearches);
                    UpdateHeroPosition(todaysSteps.AvailableSearches);
                }

            }
            catch { }
        }

        private void UpdateHeroPosition(int availableSearches)
        {
            HeroPosition = availableSearches * 14;
        }

        private void UpdateBarVisibility(int availableSearches)
        {
            if (availableSearches > 0)
                Bar1Visible = true;
            if (availableSearches > 1)
                Bar2Visible = true;
            if (availableSearches > 2)
                Bar3Visible = true;
            if (availableSearches > 3)
                Bar4Visible = true;
            if (availableSearches > 4)
                Bar5Visible = true;
            if (availableSearches > 5)
                Bar6Visible = true;
            if (availableSearches > 6)
                Bar7Visible = true;
            if (availableSearches > 7)
                Bar8Visible = true;
            if (availableSearches > 8)
                Bar9Visible = true;
            if (availableSearches > 9)
                Bar10Visible = true;
            if (availableSearches > 10)
                Bar11Visible = true;
            if (availableSearches > 11)
                Bar12Visible = true;
            if (availableSearches > 12)
                Bar13Visible = true;
            if (availableSearches > 13)
                Bar14Visible = true;
            if (availableSearches > 14)
                Bar15Visible = true;
            if (availableSearches > 15)
                Bar16Visible = true;
            if (availableSearches > 16)
                Bar17Visible = true;
            if (availableSearches > 17)
                Bar18Visible = true;
            if (availableSearches > 18)
                Bar19Visible = true;
            if (availableSearches > 19)
                Bar20Visible = true;
        }

        private int CalculateAvailableSearches(double processedStepCount)
        {
            var divisor = 500;
            if (processedStepCount < divisor)
                return 0;
            else
            {
                var s = processedStepCount / divisor;
                if (s >= 20)
                    return 20;
                else
                    return (int)s;
            }
        }

        public Command TileTapped
        {
            get
            {
                return new Command<string>((key) =>
                {
                    var playerInfo = _realmdb.All<PlayerInfo>().First();
                    var matchingTile = playerInfo?.FlippedTiles?.FirstOrDefault(x => x.TileID == key);
                    if (matchingTile == null)
                    {
                        var generator = new RewardGenerator();
                        var newTile = new FlippedTile
                        {
                            TileID = key,
                            AssociatedImage = generator.GenerateTileReward()
                        };
                        UpdateTile(newTile);
                        _realmdb.Write(() =>
                        {
                            playerInfo.SearchesCompleted++;
                            playerInfo.FlippedTiles.Add(newTile);
                        });

                    }
                },
                (nothing) => { return PlayerData.SearchesAvailable > 0; });
            }
        }

        private void UpdateTile(FlippedTile tile)
        {
            var id = Convert.ToInt32(tile.TileID);
            if (id == 1)
                Tile1 = ImageSource.FromFile(tile.AssociatedImage);
            else if(id == 2)
                Tile2 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 3)
                Tile3 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 4)
                Tile4 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 5)
                Tile5 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 6)
                Tile6 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 7)
                Tile7 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 8)
                Tile8 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 9)
                Tile9 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 10)
                Tile10 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 11)
                Tile11 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 12)
                Tile12 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 13)
                Tile13 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 14)
                Tile14 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 15)
                Tile15 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 16)
                Tile16 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 17)
                Tile17 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 18)
                Tile18 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 19)
                Tile19 = ImageSource.FromFile(tile.AssociatedImage);
            else if (id == 20)
                Tile20 = ImageSource.FromFile(tile.AssociatedImage);
        }

        private void StartStepCounter()
        {
            var ped = Xamarin.Forms.DependencyService.Get<IStepSensor>();
            ped.Start();
        }
    }
}
