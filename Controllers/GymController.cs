using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class GymController : Controller
{
    private static readonly List<GymMember> Members =
    [
        new()
        {
            Id = 1,
            Name = "Nguyen Van An",
            Email = "an.nguyen@example.com",
            Phone = "0901234567",
            PackageName = "Tap tu do",
            MonthlyFee = 500000,
            SessionsLeft = 24,
            StartDate = new DateTime(2026, 6, 1)
        },
        new()
        {
            Id = 2,
            Name = "Tran Minh Chau",
            Email = "chau.tran@example.com",
            Phone = "0912345678",
            PackageName = "PT ca nhan",
            MonthlyFee = 1800000,
            SessionsLeft = 12,
            StartDate = new DateTime(2026, 5, 20)
        },
        new()
        {
            Id = 3,
            Name = "Le Hoang Nam",
            Email = "nam.le@example.com",
            Phone = "0987654321",
            PackageName = "Yoga va Gym",
            MonthlyFee = 900000,
            SessionsLeft = 18,
            StartDate = new DateTime(2026, 5, 10)
        }
    ];

    public IActionResult Index(string? search, string? sortOrder)
    {
        ViewData["CurrentSearch"] = search;
        ViewData["NameSort"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty;
        ViewData["FeeSort"] = sortOrder == "fee" ? "fee_desc" : "fee";
        ViewData["SessionsSort"] = sortOrder == "sessions" ? "sessions_desc" : "sessions";

        IEnumerable<GymMember> members = Members;

        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim();
            members = members.Where(member =>
                member.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                member.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                member.Phone.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                member.PackageName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        members = sortOrder switch
        {
            "name_desc" => members.OrderByDescending(member => member.Name),
            "fee" => members.OrderBy(member => member.MonthlyFee),
            "fee_desc" => members.OrderByDescending(member => member.MonthlyFee),
            "sessions" => members.OrderBy(member => member.SessionsLeft),
            "sessions_desc" => members.OrderByDescending(member => member.SessionsLeft),
            _ => members.OrderBy(member => member.Name)
        };

        return View(members.ToList());
    }

    public IActionResult Detail(int id)
    {
        var member = FindMember(id);

        if (member is null)
        {
            return NotFound();
        }

        return View(member);
    }

    public IActionResult Create()
    {
        return View(new GymMember { StartDate = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(GymMember model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.Id = Members.Count == 0 ? 1 : Members.Max(member => member.Id) + 1;
        Members.Add(model);

        TempData["Message"] = "Them hoi vien thanh cong.";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var member = FindMember(id);

        if (member is null)
        {
            return NotFound();
        }

        return View(member);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(GymMember model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var member = FindMember(model.Id);

        if (member is null)
        {
            return NotFound();
        }

        member.Name = model.Name;
        member.Email = model.Email;
        member.Phone = model.Phone;
        member.PackageName = model.PackageName;
        member.MonthlyFee = model.MonthlyFee;
        member.SessionsLeft = model.SessionsLeft;
        member.StartDate = model.StartDate;

        TempData["Message"] = "Cap nhat hoi vien thanh cong.";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var member = FindMember(id);

        if (member is null)
        {
            return NotFound();
        }

        return View(member);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var member = FindMember(id);

        if (member is null)
        {
            return NotFound();
        }

        Members.Remove(member);

        TempData["Message"] = "Xoa hoi vien thanh cong.";
        return RedirectToAction(nameof(Index));
    }

    private static GymMember? FindMember(int id)
    {
        return Members.FirstOrDefault(member => member.Id == id);
    }
}
