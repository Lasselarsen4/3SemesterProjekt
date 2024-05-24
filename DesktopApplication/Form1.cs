using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            var serviceConnection = new ServiceConnection("http://localhost:5042/api/"); // Adjusted base URL
            _productService = new ProductService(serviceConnection);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            try
            {
                var products = await _productService.GetProducts();
                dataGridViewProducts.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}");
            }
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

            try
            {
                await _productService.SaveProduct(product);
                await LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}");
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxId.Text))
            {
                MessageBox.Show("Select a product to update.");
                return;
            }

            var product = new Product
            {
                ProductId = int.Parse(textBoxId.Text),
                ProductName = textBoxName.Text,
                ProductPrice = decimal.Parse(textBoxPrice.Text),
                ProductDescription = textBoxDescription.Text,
                Stock = int.Parse(textBoxStock.Text)
            };

            try
            {
                var success = await _productService.UpdateProduct(product);
                if (success)
                {
                    MessageBox.Show("Product updated successfully.");
                    await LoadProducts();
                }
                else
                {
                    MessageBox.Show("Failed to update product.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}");
            }
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxId.Text))
            {
                MessageBox.Show("Select a product to delete.");
                return;
            }

            int productId = int.Parse(textBoxId.Text);

            try
            {
                var success = await _productService.DeleteProduct(productId);
                if (success)
                {
                    MessageBox.Show("Product deleted successfully.");
                    await LoadProducts();
                }
                else
                {
                    MessageBox.Show("Failed to delete product.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting product: {ex.Message}");
            }
        }

        private void dataGridViewProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewProducts.Rows[e.RowIndex];
                textBoxId.Text = row.Cells["ProductId"].Value.ToString();
                textBoxName.Text = row.Cells["ProductName"].Value.ToString();
                textBoxPrice.Text = row.Cells["ProductPrice"].Value.ToString();
                textBoxDescription.Text = row.Cells["ProductDescription"].Value.ToString();
                textBoxStock.Text = row.Cells["Stock"].Value.ToString();
            }
        }
    }
}
