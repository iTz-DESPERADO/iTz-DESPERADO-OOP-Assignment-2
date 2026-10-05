using System;
using System.Collections.Generic;

namespace LibrarySystem;

internal class Program
{
    private static int _checks;
    private static int _expectedRejections;
    private static int _actualRejections;

    private static void Main()
    {
        DateTime date = new DateTime(2026, 1, 1);
        DateTime hired = new DateTime(2024, 6, 1);
        StudentMember student = new StudentMember("S001", "Sara Ali", "0910000001");
        StudentMember otherStudent = new StudentMember("S002", "Omar Salem", "0910000002");
        PremiumMember premium = new PremiumMember("P001", "Mona Khaled", "0910000003", 20m);
        Librarian librarian = new Librarian("E001", "Ali Hassan", "0910000004", hired, 2000m);
        HeadLibrarian head = new HeadLibrarian("E002", "Nour Ahmed", "0910000005", hired, 3000m);
        Shelver shelver = new Shelver("E003", "Huda Omar", "0910000006", hired, 1500m, "Fiction");

        Book book = new Book("B001", "C# Basics", 2m);
        DVD dvd = new DVD("D001", "Science Documentary", 2m);
        Magazine magazine = new Magazine("M001", "City Reader", 2m);
        Book fourthBook = new Book("B002", "Learning Inheritance", 2m);

        Console.WriteLine("PART 03 - LIBRARY SYSTEM: DESIGN WITH INHERITANCE");
        Console.WriteLine("1. Compile-time protection (examples intentionally commented out)");

        // must NOT compile: parent constructors are protected.
        //new Person("X", "Name", "Phone");
        //new Member("X", "Name", "Phone", 3, 0m);
        //new Staff("X", "Name", "Phone", hired, 1000m, 0m);
        //new LibraryItem("X", "Title", 1m, 7, 1m);
        //must NOT compile: identities and derived values are get-only.
        //student.FullName = "Changed";
        //student.PersonId = "Changed";
        //student.Phone = "Changed";
        //librarian.HireDate = date;
        //premium.ReadingPoints = 100;
        //premium.DiscountPercentage = 50m;
        //book.CatalogNumber = "Changed";
        //book.Title = "Changed";
        //must NOT compile: the collection and setters are protected.
        // student.Loans.Add(null);
        // student.Loans.Remove(null);
        // book.IsOnLoan = true;
        // book.IsWithdrawn = true;
        // book.BaseLateFee = 0m;
        // librarian.MonthlySalary = 5000m;
        // shelver.Section = "History";

        Console.WriteLine("\n2. One pay method for a List<Staff>");
        List<Staff> staff = new List<Staff> { librarian, head, shelver };
        foreach (Staff employee in staff)
            Console.WriteLine($"{employee.FullName}: salary {employee.MonthlySalary:F2}, pay {employee.GetMonthlyPay():F2}");
        Check(librarian.GetMonthlyPay() == 2000m, "Librarian receives salary only");
        Check(head.GetMonthlyPay() == 3400m, "Head receives the fixed 400 allowance");
        Check(shelver.GetMonthlyPay() == 1500m, "Shelver receives salary only");
        head.GiveRaise(10m);
        Check(head.MonthlySalary == 3300m && head.GetMonthlyPay() == 3700m, "Raise changes salary; allowance stays 400");
        shelver.Reassign("Children");
        Check(shelver.Section == "Children", "Shelver is reassigned through an action");

        foreach (decimal invalid in new List<decimal> { 0m, -10m })
        {
            BeginFailure("Nonpositive raise");
            try { head.GiveRaise(invalid); }
            catch (ArgumentException ex) { Rejected(ex); }
        }
        Check(head.MonthlySalary == 3300m, "Rejected raises preserve salary");
        BeginFailure("Blank section");
        try { shelver.Reassign(" "); }
        catch (ArgumentException ex) { Rejected(ex); }
        Check(shelver.Section == "Children", "Rejected reassignment preserves section");

        Console.WriteLine("\n3. One daily-fee method for a List<LibraryItem>");
        List<LibraryItem> items = new List<LibraryItem> { book, dvd, magazine };
        foreach (LibraryItem item in items)
            Console.WriteLine($"{item.Title}: {item.LoanPeriodDays} days, daily late fee {item.GetDailyLateFee():F2}");
        Check(book.LoanPeriodDays == 21 && book.GetDailyLateFee() == 2m, "Book: 21 days and base fee");
        Check(dvd.LoanPeriodDays == 7 && dvd.GetDailyLateFee() == 4m, "DVD: 7 days and double fee");
        Check(magazine.LoanPeriodDays == 3 && magazine.GetDailyLateFee() == 1m, "Magazine: 3 days and half fee");
        head.ChangeLateFee(book, 3m);
        Check(book.BaseLateFee == 3m && book.GetDailyLateFee() == 3m, "Head changes item pricing");
        foreach (decimal invalid in new List<decimal> { 0m, -1m })
        {
            BeginFailure("Nonpositive replacement fee");
            try { head.ChangeLateFee(book, invalid); }
            catch (ArgumentException ex) { Rejected(ex); }
        }
        Check(book.BaseLateFee == 3m, "Rejected pricing preserves the old fee");

        Console.WriteLine("\n4. Withdrawn items, double borrowing, and the student limit");
        head.WithdrawItem(book);
        BeginFailure("Borrow a withdrawn item");
        try { student.Borrow("L001", book, date); }
        catch (InvalidOperationException ex) { Rejected(ex); }
        Check(student.Loans.Count == 0 && !book.IsOnLoan, "Failed borrowing leaves history and availability unchanged");
        head.RestoreItem(book);
        Loan firstLoan = student.Borrow("L001", book, date);
        Check(!book.IsWithdrawn && book.IsOnLoan, "Restored item can be borrowed");
        Check(ReferenceEquals(firstLoan.Item, book) && ReferenceEquals(firstLoan.Member, student), "Loan holds the real item and member");
        Check(firstLoan.DueDate == new DateTime(2026, 1, 22), "Book due date is calculated");
        Check(firstLoan.LateFee == 0m && firstLoan.ReturnDate == null, "Unreturned loan has no return date or fee");

        // must NOT compile: loan metadata is immutable or privately set.
        // firstLoan.LoanId = "Changed";
        // firstLoan.BorrowDate = date;
        // firstLoan.Member = otherStudent;
        // firstLoan.Item = dvd;
        // firstLoan.Status = LoanStatus.Returned;
        // firstLoan.ReturnDate = date;
        // firstLoan.DueDate = date;
        // firstLoan.LateFee = 100m;

        BeginFailure("Borrow an item already on loan");
        try { otherStudent.Borrow("DOUBLE", book, date); }
        catch (InvalidOperationException ex) { Rejected(ex); }
        Check(otherStudent.Loans.Count == 0, "Double borrowing adds no history entry");
        Loan studentDvdLoan = student.Borrow("L002", dvd, date);
        Loan studentMagazineLoan = student.Borrow("L003", magazine, date);
        Check(studentMagazineLoan.DueDate == new DateTime(2026, 1, 4), "Magazine due date uses three days");
        BeginFailure("Student borrows a fourth item");
        try { student.Borrow("L004", fourthBook, date); }
        catch (InvalidOperationException ex) { Rejected(ex); }
        Check(student.ActiveLoanCount == 3 && student.Loans.Count == 3 && !fourthBook.IsOnLoan,
            "Student limit is enforced without changing the fourth item");

        Console.WriteLine("\n5. Valid return dates, on-time fees, and full history");
        BeginFailure("Return before the borrow date");
        try { librarian.ProcessReturn(studentDvdLoan, date.AddDays(-1)); }
        catch (ArgumentException ex) { Rejected(ex); }
        Check(studentDvdLoan.Status == LoanStatus.Borrowed && studentDvdLoan.ReturnDate == null && dvd.IsOnLoan,
            "Invalid return date preserves loan and item state");
        librarian.ProcessReturn(studentDvdLoan, studentDvdLoan.DueDate);
        Check(studentDvdLoan.LateFee == 0m && !dvd.IsOnLoan, "Returning on the due date is free and releases the item");
        Check(student.Loans.Count == 3 && student.ActiveLoanCount == 2, "Returned loans stay in history");
        Loan fourthLoan = student.Borrow("L004", fourthBook, date.AddDays(7));
        Check(student.Loans.Count == 4 && student.ActiveLoanCount == 3, "A return frees capacity for a new loan");

        Console.WriteLine("\n6. Premium DVD returned five days late");
        Loan premiumLoan = premium.Borrow("P-L001", dvd, new DateTime(2026, 2, 1));
        Check(premium.ReadingPoints == 0, "Borrowing alone earns no reading points");
        librarian.ProcessReturn(premiumLoan, premiumLoan.DueDate.AddDays(5));
        Console.WriteLine($"Borrow date: {premiumLoan.BorrowDate:yyyy-MM-dd}");
        Console.WriteLine($"Due date:    {premiumLoan.DueDate:yyyy-MM-dd}");
        Console.WriteLine($"Return date: {premiumLoan.ReturnDate:yyyy-MM-dd}");
        Console.WriteLine($"Late fee: {premiumLoan.LateFee:F2} (5 days x 4.00 x 80%)");
        Console.WriteLine($"Reading points: {premium.ReadingPoints}");
        Check(premiumLoan.DueDate == new DateTime(2026, 2, 8), "DVD due date is calculated from seven days");
        Check(premiumLoan.LateFee == 16m, "Premium discount gives a late fee of 16.00");
        Check(premium.ReadingPoints == 5, "Each returned loan earns five points");

        BeginFailure("Return the same loan twice");
        try { librarian.ProcessReturn(premiumLoan, premiumLoan.DueDate.AddDays(6)); }
        catch (InvalidOperationException ex) { Rejected(ex); }
        BeginFailure("Mark a returned loan as lost");
        try { librarian.MarkAsLost(premiumLoan); }
        catch (InvalidOperationException ex) { Rejected(ex); }
        Check(premiumLoan.Status == LoanStatus.Returned && premiumLoan.LateFee == 16m && premium.ReadingPoints == 5,
            "Rejected transitions preserve status, return date, fee, and points");

        Loan earlyLoan = premium.Borrow("P-L002", dvd, new DateTime(2026, 3, 1));
        librarian.ProcessReturn(earlyLoan, earlyLoan.BorrowDate);
        Check(earlyLoan.LateFee == 0m && premium.ReadingPoints == 10, "Early returns are free and also earn points");
        Check(premium.Loans.Count == 2, "Premium history retains both returns of the same item");

        Console.WriteLine("\n7. Lost loans and withdrawal during an existing loan");
        librarian.MarkAsLost(studentMagazineLoan);
        Check(studentMagazineLoan.Status == LoanStatus.Lost && student.ActiveLoanCount == 2,
            "Lost loans do not count as active loans");
        Check(magazine.IsOnLoan && studentMagazineLoan.ReturnDate == null, "Lost item remains unreturned and unavailable");
        BeginFailure("Return a lost loan");
        try { librarian.ProcessReturn(studentMagazineLoan, date.AddDays(5)); }
        catch (InvalidOperationException ex) { Rejected(ex); }
        BeginFailure("Mark a lost loan as lost again");
        try { librarian.MarkAsLost(studentMagazineLoan); }
        catch (InvalidOperationException ex) { Rejected(ex); }
        BeginFailure("Borrow a lost item");
        try { otherStudent.Borrow("LOST-ITEM", magazine, date.AddDays(6)); }
        catch (InvalidOperationException ex) { Rejected(ex); }
        head.WithdrawItem(fourthBook);
        librarian.ProcessReturn(fourthLoan, fourthLoan.DueDate.AddDays(1));
        Check(fourthLoan.LateFee == 2m, "Student pays the full daily fee with no discount");
        Check(fourthBook.IsWithdrawn && !fourthBook.IsOnLoan, "Returning a withdrawn item keeps it withdrawn");
        head.RestoreItem(fourthBook);
        Loan restoredLoan = otherStudent.Borrow("RESTORED", fourthBook, new DateTime(2026, 3, 1));
        Check(restoredLoan.Status == LoanStatus.Borrowed, "Restoration permits a later loan");

        Console.WriteLine("\n8. Creation validation and unique identifiers");
        BeginFailure("Empty person ID");
        try { new StudentMember("", "Name", "Phone"); }
        catch (ArgumentException ex) { Rejected(ex); }
        BeginFailure("Empty full name");
        try { new StudentMember("INVALID-NAME", " ", "Phone"); }
        catch (ArgumentException ex) { Rejected(ex); }
        BeginFailure("Empty phone");
        try { new StudentMember("INVALID-PHONE", "Name", ""); }
        catch (ArgumentException ex) { Rejected(ex); }
        BeginFailure("Duplicate person ID across member and staff types");
        try { new Librarian("S001", "Another person", "Phone", hired, 1000m); }
        catch (ArgumentException ex) { Rejected(ex); }
        foreach (decimal invalid in new List<decimal> { -1m, 101m })
        {
            BeginFailure("Discount outside 0 through 100");
            try { new PremiumMember("BAD-DISCOUNT", "Name", "Phone", invalid); }
            catch (ArgumentException ex) { Rejected(ex); }
        }
        foreach (decimal invalid in new List<decimal> { 0m, -1m })
        {
            BeginFailure("Nonpositive initial salary");
            try { new Librarian("BAD-SALARY", "Name", "Phone", hired, invalid); }
            catch (ArgumentException ex) { Rejected(ex); }
            BeginFailure("Nonpositive initial fee");
            try { new Book("BAD-FEE", "Title", invalid); }
            catch (ArgumentException ex) { Rejected(ex); }
        }
        BeginFailure("Empty initial section");
        try { new Shelver("SECTION-RETRY", "Name", "Phone", hired, 1000m, ""); }
        catch (ArgumentException ex) { Rejected(ex); }
        Shelver retry = new Shelver("SECTION-RETRY", "Name", "Phone", hired, 1000m, "History");
        Check(retry.Section == "History", "Invalid constructor did not consume the person ID");
        BeginFailure("Empty catalog number");
        try { new Book("", "Title", 1m); }
        catch (ArgumentException ex) { Rejected(ex); }
        BeginFailure("Empty item title");
        try { new Book("BAD-TITLE", " ", 1m); }
        catch (ArgumentException ex) { Rejected(ex); }
        BeginFailure("Duplicate catalog number across item types");
        try { new DVD("B001", "Different title", 1m); }
        catch (ArgumentException ex) { Rejected(ex); }

        BeginFailure("Empty loan ID");
        try { otherStudent.Borrow("", dvd, date); }
        catch (ArgumentException ex) { Rejected(ex); }
        BeginFailure("Duplicate loan ID across members");
        try { otherStudent.Borrow("L001", dvd, date); }
        catch (ArgumentException ex) { Rejected(ex); }
        BeginFailure("Borrow date cannot produce a due date");
        try { otherStudent.Borrow("DATE-RETRY", dvd, DateTime.MaxValue); }
        catch (ArgumentException ex) { Rejected(ex); }
        Check(!dvd.IsOnLoan && otherStudent.Loans.Count == 1, "Invalid loan data leaves availability and history unchanged");
        Loan validDateLoan = otherStudent.Borrow("DATE-RETRY", dvd, date.AddHours(12));
        Check(validDateLoan.BorrowDate == date, "Calendar dates discard time components");
        librarian.ProcessReturn(validDateLoan, validDateLoan.DueDate);

        Console.WriteLine("\n9. Premium loan limit and discount boundaries");
        foreach (string number in new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" })
        {
            Book limitBook = new Book("LIMIT-B" + number, "Premium book " + number, 1m);
            premium.Borrow("LIMIT-L" + number, limitBook, date);
        }
        Book eleventhBook = new Book("LIMIT-B11", "Eleventh premium book", 1m);
        BeginFailure("Premium member borrows an eleventh active item");
        try { premium.Borrow("LIMIT-L11", eleventhBook, date); }
        catch (InvalidOperationException ex) { Rejected(ex); }
        Check(premium.ActiveLoanCount == 10 && premium.Loans.Count == 12 && !eleventhBook.IsOnLoan,
            "Premium limit is ten active loans, independent of historical returns");
        librarian.MarkAsLost(premium.Loans[2]);
        Check(premium.ReadingPoints == 10 && premium.ActiveLoanCount == 9, "Lost premium loan earns no points and frees capacity");
        premium.Borrow("LIMIT-L11", eleventhBook, date);
        Check(premium.ActiveLoanCount == 10, "A lost loan frees a slot for a different item");

        PremiumMember noDiscount = new PremiumMember("P-ZERO", "Zero Discount", "Phone", 0m);
        PremiumMember fullDiscount = new PremiumMember("P-FULL", "Full Discount", "Phone", 100m);
        Magazine halfFee = new Magazine("M-HALF", "Magazine fee example", 3m);
        Loan noDiscountLoan = noDiscount.Borrow("ZERO-DISCOUNT", halfFee, date);
        librarian.ProcessReturn(noDiscountLoan, noDiscountLoan.DueDate.AddDays(1));
        Check(noDiscountLoan.LateFee == 1.5m, "Magazine multiplier and zero discount preserve decimal precision");
        Loan fullDiscountLoan = fullDiscount.Borrow("FULL-DISCOUNT", halfFee, date.AddDays(10));
        librarian.ProcessReturn(fullDiscountLoan, fullDiscountLoan.DueDate.AddDays(5));
        Check(fullDiscountLoan.LateFee == 0m && fullDiscount.ReadingPoints == 5, "A 100 percent discount removes the fee");

        Check(_actualRejections == _expectedRejections, "Every invalid operation was rejected");
        Console.WriteLine($"\nSUCCESS: {_checks} checks passed; {_actualRejections} expected failures caught.");
    }

    private static void Check(bool condition, string description)
    {
        if (!condition)
            throw new InvalidOperationException("CHECK FAILED: " + description);
        _checks++;
        Console.WriteLine("PASS: " + description);
    }

    private static void BeginFailure(string description)
    {
        _expectedRejections++;
        Console.WriteLine("Expect rejection: " + description);
    }

    private static void Rejected(Exception exception)
    {
        _actualRejections++;
        Console.WriteLine("  Rejected: " + exception.Message);
    }
}
