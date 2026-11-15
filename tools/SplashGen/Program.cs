// Usage: dotnet run --project tools/SplashGen -- --version 2026.3.234 --out Assets/splash.png
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

var o = new Dictionary<string, string>
{
    ["title"] = "Clinic Manager",
    ["year"] = "2027",                                   // product name year
    ["version"] = "2026.3.234",                          // build version
    ["copyrightYear"] = DateTime.Now.Year.ToString(),    // copyright year
    ["company"] = "Component Systems",
    ["tagline"] = "Patients  •  Appointments  •  Billing  •  Records",
    ["font"] = "Segoe UI",
    ["out"] = "splash.png",
};
for (int i = 0; i + 1 < args.Length; i += 2)
    if (args[i].StartsWith("--")) o[args[i][2..]] = args[i + 1];
o["version"] = o["version"].TrimStart('v', 'V');

const int W = 800, H = 500;
Color C(string hex) => ColorTranslator.FromHtml(hex);
SolidBrush B(string hex) => new SolidBrush(C(hex));
Font F(float px, FontStyle st = FontStyle.Regular) => new Font(o["font"], px, st, GraphicsUnit.Pixel);

using var bmp = new Bitmap(W, H, PixelFormat.Format32bppArgb);
bmp.SetResolution(96, 96);
using var g = Graphics.FromImage(bmp);
g.SmoothingMode = SmoothingMode.AntiAlias;
g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
g.PixelOffsetMode = PixelOffsetMode.HighQuality;
g.Clear(C("#0B3C5D"));

static GraphicsPath Round(float x, float y, float w, float h, float r)
{
    float d = r * 2;
    var p = new GraphicsPath();
    p.AddArc(x, y, d, d, 180, 90);
    p.AddArc(x + w - d, y, d, d, 270, 90);
    p.AddArc(x + w - d, y + h - d, d, d, 0, 90);
    p.AddArc(x, y + h - d, d, d, 90, 90);
    p.CloseFigure();
    return p;
}
void FillRound(string hex, float x, float y, float w, float h, float r)
{
    using var br = B(hex); using var p = Round(x, y, w, h, r);
    g.FillPath(br, p);
}
void Oval(string hex, float x, float y, float w, float h) { using var b = B(hex); g.FillEllipse(b, x, y, w, h); }

// background shapes
Oval("#1B8A8F", -120, -140, 380, 380);
Oval("#3CC4A8", -60, -80, 210, 210);
Oval("#1B8A8F", 560, 300, 400, 400);
Oval("#4FA8E0", 640, 380, 260, 260);
using (var b = B("#124E75"))
    g.FillPolygon(b, new[] { new PointF(800, 0), new PointF(800, 170), new PointF(630, 0) });
for (int r = 0; r < 4; r++)
    for (int c = 0; c < 6; c++)
        Oval("#1D5F85", 560 + c * 18, 190 + r * 18, 6, 6);

// logo badge
float bx = 80, by = 140, bs = 150;
FillRound("#082B42", bx + 8, by + 8, bs, bs, 34);
FillRound("#FFFFFF", bx, by, bs, bs, 34);
float cx = bx + bs / 2, cy = by + bs / 2 - 8, t = 22, L = 46;
FillRound("#FF6B6B", cx - t, cy - L, t * 2, L * 2, 6);
FillRound("#FF6B6B", cx - L, cy - t, L * 2, t * 2, 6);
FillRound("#3CC4A8", cx - t + 6, cy - L + 6, (t - 6) * 2, (L - 6) * 2, 3);
FillRound("#3CC4A8", cx - L + 6, cy - t + 6, (L - 6) * 2, (t - 6) * 2, 3);
FillRound("#FFFFFF", cx - t, cy - t, t * 2, t * 2, 4);
float y0 = by + bs - 26;
var beat = new (float x, float y)[] { (22, 0), (55, 0), (68, -18), (82, 14), (95, -8), (104, 0), (bs - 22, 0) }
    .Select(p => new PointF(bx + p.x, y0 + p.y)).ToArray();
using (var pen = new Pen(C("#0B3C5D"), 4) { LineJoin = LineJoin.Round, StartCap = LineCap.Round, EndCap = LineCap.Round })
    g.DrawLines(pen, beat);

// title block
float tx = 270;
using (var f = F(54, FontStyle.Bold)) { using var b = B("#FFFFFF"); g.DrawString(o["title"], f, b, tx - 4, 146); }
using (var f = F(54, FontStyle.Bold)) { using var b = B("#F6C85F"); g.DrawString(o["year"], f, b, tx - 4, 211); }
FillRound("#3CC4A8", tx, 292, 90, 6, 3);
using (var f = F(16)) { using var b = B("#D9ECF2"); g.DrawString(o["tagline"], f, b, tx, 308); }

// ---- footer band: version (left) + copyright (right) ----
const float bandTop = H - 64, bandH = 50;
using (var b = B("#082B42")) g.FillRectangle(b, 0, bandTop, W, bandH);
using (var b = B("#3CC4A8")) g.FillRectangle(b, 0, H - 14, W, 14);
using (var b = B("#F6C85F")) g.FillRectangle(b, 0, H - 14, W * .33f, 14);
using (var b = B("#FF6B6B")) g.FillRectangle(b, W * .33f, H - 14, W * .22f, 14);

var mid = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
var midRight = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

// version pill
string verText = $"Version {o["version"]}";
using (var f = F(14, FontStyle.Bold))
{
    float pw = g.MeasureString(verText, f).Width + 10, ph = 28, px = 30, py = bandTop + (bandH - ph) / 2;
    FillRound("#3CC4A8", px, py, pw, ph, ph / 2);
    using var b = B("#0B3C5D");
    g.DrawString(verText, f, b, new RectangleF(px, py, pw, ph), mid);
}

// copyright
string copyText = $"© {o["copyrightYear"]} {o["company"]}. All rights reserved.";
using (var f = F(13)) { using var b = B("#D9ECF2"); g.DrawString(copyText, f, b, new RectangleF(0, bandTop, W - 30, bandH), midRight); }

Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(o["out"]))!);
bmp.Save(o["out"], ImageFormat.Png);
Console.WriteLine("saved " + o["out"]);
