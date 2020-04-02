using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microsoft.Azure.Cosmos;
using System.Configuration;

namespace SOFT262_Coursework
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        // The Azure Cosmos DB endpoint for running this project.
        private static readonly string EndpointUri = "https://alexanderpritchard.documents.azure.com:443/";

        // The primary key for the Azure Cosmos account.
        private static readonly string PrimaryKey = "EO0WLSM5suYrfVi2un9ttrVvnzU9tarosGLRSTtvm8gV9fgWWVYcbuCXR55bVt8iTitWFj2upNyabKFA0X2d2w==";

        // The Cosmos client instance
        private CosmosClient cosmosClient;

        // The database we will create
        private Database database;

        // The name of the database and container we will create
        private string databaseId = "ListOfPlanetsDB";
        private string containerId = "Items";

        // The container we will create.
        private Microsoft.Azure.Cosmos.Container container;

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
