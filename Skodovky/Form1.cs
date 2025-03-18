using System.Data;
using System.Xml.Linq;

namespace Skodovky
{
    public partial class Form1 : Form
    {
        private List<CarSale> sales = new List<CarSale>(); // Uložení dat z XML souboru

        public Form1()
        {
            InitializeComponent();

            //Schováme tabulky a combo box než se vybere soubor
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            comboBox1.Visible = false;
            errorLabel.Visible = false;

            dataGridView1.CellFormatting += dataGridView_CellFormatting;
            dataGridView2.CellFormatting += dataGridView_CellFormatting;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Pokud není vybrán žádný model, return early
            if (comboBox1.SelectedItem == null) return;

            // Získání vybraného modelu
            string selectedModel = comboBox1.SelectedItem.ToString();

            // Filtrovaní prodejů podle vybraného modelu a víkendu
            var filteredSales = sales
                .Where(s => s.Nazev == selectedModel && IsWeekend(s.Datum))
                .ToList();

            if (!filteredSales.Any())
            {
                // Zobrazení zprávy pokud nejsou žádné prodeje
                errorLabel.Text = "Žádné prodeje tohoto modelu přes víkend.";
                errorLabel.ForeColor = Color.Red; // Červená barva pro zvýraznění
                dataGridView2.DataSource = null; // Vyčištění tabulky
            }
            else
            {
                errorLabel.Text = ""; // Vymažeme chybovou zprávu

                // Spočítání celkové ceny a ceny s DPH
                double totalCena = filteredSales.Sum(s => s.Cena);
                double totalCenaDPH = totalCena * (1 + filteredSales.First().DPH / 100.0);

                // Vytvoření nové tabulky pro zobrazení
                DataTable summaryTable = new DataTable();
                summaryTable.Columns.Add("Model", typeof(string));
                summaryTable.Columns.Add("CenaSDPH", typeof(double));

                // Přidání dat ve dvou řádcích
                summaryTable.Rows.Add(selectedModel, DBNull.Value);  // Název modelu na první řádek
                summaryTable.Rows.Add(totalCena, totalCenaDPH);       // Ceny na druhý řádek

                // Přiřazení dat do tabulky
                dataGridView2.DataSource = summaryTable;

                // Formátování záhlaví sloupců pro víceřádkový text
                dataGridView2.Columns["Model"].HeaderText = "Název modelu\nCena bez DPH";
                dataGridView2.Columns["CenaSDPH"].HeaderText = "Cena s DPH";

                // Povolení word wrap pro víceřádkové záhlaví
                dataGridView2.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Automatická úprava výšky řádků pro správné zobrazení
                dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            }
        }


        // Helper funkce pro určení zda je datum víkend
        private bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView grid = sender as DataGridView; // Určení, která tabulka volá funkci
            if (grid == null || grid.DataSource == null || e.ColumnIndex < 0 || grid.Columns.Count == 0)
            {
                return; // Ochrana proti chybám
            }

            // Získáme název sloupce pro formátování
            string columnName = grid.Columns[e.ColumnIndex].Name;
            //TODO: formátování ceny bez dph-----
            if (columnName == "Cena" || columnName == "CenaDPH" || columnName == "DPH" || columnName == "CenaSDPH" || columnName == "Název modelu\nCena bez DPH")
            {
                if (e.Value is double value)
                {
                    if (columnName == "Cena" || columnName == "CenaDPH" || columnName == "CenaSDPH" || columnName == "Název modelu\nCena bez DPH")
                    {
                        e.Value = $"{value:N0} Kč"; // Přidáme Kč a tisícový oddělovač
                    }
                    else if (columnName == "DPH")
                    {
                        e.Value = $"{value}%"; // Přidání % k DPH
                    }
                    e.FormattingApplied = true;
                }
            }
        }


        // Otevření dialogu pro výběr souboru
        private void selectXMLFile(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML Files (*.xml)|*.xml"; // Pouze XML soubory

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    LoadXmlFile(filePath);
                }
            }
        }
        // Načtení dat z XML souboru
        private void LoadXmlFile(string filePath)
        {
            try
            {
                // Základní validace že soubor obsahuje očekávané elementy
                XDocument doc = XDocument.Load(filePath);

                if (!doc.Descendants("nazev").Any() ||
                    !doc.Descendants("datum").Any() ||
                    !doc.Descendants("cena").Any() ||
                    !doc.Descendants("DPH").Any())

                {
                    MessageBox.Show("Neplatná struktura XML souboru. Ujistěte se, že soubor obsahuje správná data.",
                                    "Chyba načítání", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit early
                }

                // Pokud je soubor validní, načteme data
                CarSalesProcessor processor = new CarSalesProcessor(filePath);
                sales = processor.LoadSales();

                dataGridView1.DataSource = sales; // Update the main table
                //dataGridView2.DataSource = null; // Clear the second table

                // Formátování sloupců pro cenu s DPH
                dataGridView1.Columns["CenaDPH"].HeaderText = "Cena s DPH";
                //dataGridView2.Columns["CenaDPH"].HeaderText = "Cena s DPH";


                // Populace combo boxu s unikátními modely
                var uniqueModels = sales.Select(s => s.Nazev).Distinct().ToList();
                comboBox1.DataSource = uniqueModels;
                comboBox1.SelectedIndex = -1; // Žádná položka není vybrána defaultně

                dataGridView2.DataSource = null; // Druhá tabulka se inicializuje až po výběru modelu

                // Zobrazíme UI elementy po načtení dat
                dataGridView1.Visible = true;
                dataGridView2.Visible = true;
                comboBox1.Visible = true;
                errorLabel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error načítání souboru: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Dark mode
        private void darkMode_CheckedChanged(object sender, EventArgs e)
        {
            bool isDarkMode = darkMode.Checked;

            // Změníme barvu pozadí formuláře
            this.BackColor = isDarkMode ? Color.FromArgb(45, 45, 48) : SystemColors.Control;

            // Změníme barvu pozadí tabulek
            dataGridView1.BackgroundColor = isDarkMode ? Color.FromArgb(30, 30, 30) : SystemColors.Window;
            dataGridView2.BackgroundColor = isDarkMode ? Color.FromArgb(30, 30, 30) : SystemColors.Window;

            // Změníme barvu buněk - dark mode
            DataGridViewCellStyle darkCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(50, 50, 50), // Šedé pozadí
                ForeColor = Color.White, // Bílý text
                SelectionBackColor = Color.FromArgb(70, 70, 70), // Světlejší šedé pozadí pro vybraný řádek
                SelectionForeColor = Color.White
            };
            // Změníma barvu buněk - light mode
            DataGridViewCellStyle lightCellStyle = new DataGridViewCellStyle
            {
                BackColor = SystemColors.Window, // Default
                ForeColor = Color.Black,
                SelectionBackColor = SystemColors.Highlight,
                SelectionForeColor = SystemColors.HighlightText
            };

            // Obě tabulky mají stejný styl
            dataGridView1.DefaultCellStyle = isDarkMode ? darkCellStyle : lightCellStyle;
            dataGridView2.DefaultCellStyle = isDarkMode ? darkCellStyle : lightCellStyle;

            // Darkmode checkbox barva textu - také měníme
            darkMode.ForeColor = isDarkMode ? Color.White : Color.Black;
        }


        // Export do CSV
        private void exportToCSV(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("Žádná data pro export.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Uložit jako CSV",
                FileName = "WeekendSales.csv"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    // Write headers
                    string headers = string.Join(",", dataGridView2.Columns.Cast<DataGridViewColumn>().Select(col => col.HeaderText));
                    writer.WriteLine(headers);

                    // Write rows
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            List<string> rowData = new List<string>();

                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (cell.Value is DateTime dateValue)
                                {
                                    rowData.Add(dateValue.ToString("yyyy-MM-dd")); // Format date properly
                                }
                                else
                                {
                                    rowData.Add(cell.Value?.ToString() ?? ""); // Handle other values normally
                                }
                            }

                            writer.WriteLine(string.Join(",", rowData));
                        }
                    }
                }
                MessageBox.Show("Uloženo", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            // Vynulování tabulek
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;

            // Vynulování combo boxu
            comboBox1.DataSource = null;
            comboBox1.SelectedIndex = -1;

            // Vyčištění dat
            sales.Clear();

            // Skrytí UI elementů
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            comboBox1.Visible = false;
            errorLabel.Visible = false;
        }
    }
}
