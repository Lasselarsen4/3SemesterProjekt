using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DesktopApplication.Models;
using DesktopApplication.ServiceLayer;

namespace DesktopApplication
{
    public partial class Form1 : Form
    {
        private readonly IProductService _productService;

        public Form1()
        {
            InitializeComponent();
            _productService = new ProductService();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var products = await _productService.GetProducts();
            dataGridViewProducts.DataSource = products;
        }

        private async void buttonAdd_Click(object sender, EventArgs e)
        {
            var product = new Product
            {
                ProductName = textBoxName.Text,
                ProductPrice = decimal.Parse(textBoxPrice.Text),
                ProductDescription = textBoxDescription.Text,
                Stock = int.Parse(textBoxStock.Text)
            };
            await _productService.SaveProduct(product);
            await LoadProducts();
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            var product = new Product
            {
                ProductId = int.Parse(textBoxId.Text),
                ProductName = textBoxName.Text,
                ProductPrice = decimal.Parse(textBoxPrice.Text),
                ProductDescription = textBoxDescription.Text,
                Stock = int.Parse(textBoxStock.Text)
            };
            await _productService.UpdateProduct(product);
            await LoadProducts();
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            int productId = int.Parse(textBoxId.Text);
            await _productService.DeleteProduct(productId);
            await LoadProducts();
        }

        private async void dataGridViewProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewProducts.Rows[e.RowIndex];
                textBoxName.Text = row.Cells["ProductName"].Value.ToString();
                textBoxPrice.Text = row.Cells["ProductPrice"].Value.ToString();
                textBoxDescription.Text = row.Cells["ProductDescription"].Value.ToString();
                textBoxStock.Text = row.Cells["Stock"].Value.ToString();
            }
        }
    }
}
