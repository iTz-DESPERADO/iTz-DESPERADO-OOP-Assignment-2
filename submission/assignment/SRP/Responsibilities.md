# Responsibilities

In this file I wrote the different responsibilities I found in each class before refactoring it.

## 1. WardBoard

Responsibilities:

- Store which patient is currently assigned to each bed and their acuity score.
- Calculate the acuity score from heart rate and SpO2.
- Decide if a pager alert should be sent.
- Format and store pager messages.
- Decide the patient status like `STABLE`, `WATCH`, or `ESCALATE`.
- Build the nurse handoff note.
- Export the current ward data as CSV.

Why this is a problem:

All of these things can change for different reasons. For example, the acuity calculation can change without changing the CSV format, and the pager rules can change without changing how beds are stored. Keeping all of them inside `WardBoard` gives the class more than one reason to change.

Refactored into:

- `WardRegistry`
- `AcuityScorer`
- `AcuityStatusPolicy`
- `PagerPolicy`
- `PagerMessageFormatter`
- `PagerLog`
- `HandoffNoteFormatter`
- `CensusCsvExporter`
- `WardBoard` is kept as the main class that connects them together.

## 2. CheckoutBasket

Responsibilities:

- Store basket items, coupon text, and gift-wrap state.
- Calculate the subtotal and final total.
- Read the coupon and calculate the discount.
- Calculate the gift-wrap cost.
- Build the gift card message.
- Generate the fake payment authorization code.

Why this is a problem:

The coupon rules can change without changing the basket data. Gift-wrap pricing can also change separately, and payment authorization has nothing to do with formatting a gift message. These responsibilities should not all be inside the same class.

Refactored into:

- `BasketState`
- `CouponDiscountPolicy`
- `GiftWrapPricingPolicy`
- `BasketPricingCalculator`
- `GiftMessageCardFormatter`
- `PaymentAuthorizationStub`
- `CheckoutBasket` connects the parts together.

## 3. SupportTicket

Responsibilities:

- Store the ticket information.
- Append new customer messages.
- Calculate the ticket priority from the text.
- Calculate the SLA deadline and check if it was breached.
- Build the public reply.
- Build the internal escalation message.

Why this is a problem:

Priority rules, SLA rules, and message formatting can all change separately. A change in the customer reply should not affect how priority is calculated.

Refactored into:

- `TicketPriorityClassifier`
- `SlaPolicy`
- `PublicReplyFormatter`
- `InternalEscalationFormatter`
- `SupportTicket` keeps the ticket data and uses the other classes.

## 4. LoanDesk

Responsibilities:

- Store loan application information.
- Calculate the risk score.
- Decide if the loan is accepted or rejected.
- Decide which documents are required.
- Build the decision letter.
- Export the application data as CSV.

Why this is a problem:

The risk formula, approval rules, required documents, decision letter, and CSV format can all change separately. They should not all be handled by one class.

Refactored into:

- `LoanApplication`
- `LoanRiskCalculator`
- `LoanEligibilityPolicy`
- `RequiredDocumentPolicy`
- `LoanDecisionLetterFormatter`
- `UnderwriterCsvExporter`
- `LoanDesk` connects the parts together.

## 5. CourseEnrollmentDesk

Responsibilities:

- Store enrolled students and students on the waitlist.
- Register students.
- Calculate the waitlist position.
- Move students from the waitlist when a seat becomes available.
- Build the welcome packet.
- Calculate tuition tax and the invoice total.
- Format the tuition invoice information.

Why this is a problem:

Enrollment and waitlist rules are different from formatting a welcome packet or an invoice. Tax rates and rounding can also change without changing the invoice text. These parts can change separately.

Refactored into:

- `CourseEnrollmentRegistry`
- `WelcomePacketFormatter`
- `TuitionInvoice` carries the calculated invoice values.
- `TuitionInvoiceCalculator` owns tuition tax, rounding, and totals.
- `TuitionInvoiceFormatter` formats the calculated values.
- `CourseEnrollmentDesk` connects them together.

## 6. KitchenTicket

Responsibilities:

- Store kitchen items and ingredients.
- Check the ingredients for allergens.
- Calculate the estimated ready time.
- Decide which expo lane should be used.
- Format the ticket for the thermal printer.

Why this is a problem:

Allergen rules, cooking-time calculations, expo rules, and printer formatting are different responsibilities. A change in one of them should not require changing the others.

Refactored into:

- `KitchenOrder`
- `AllergenDetector`
- `KitchenEtaEstimator`
- `ExpoLanePolicy`
- `ThermalTicketRenderer`
- `KitchenTicket` connects the parts together.

## 7. SubscriptionBilling

Responsibilities:

- Store subscription billing information and failed-payment count.
- Calculate prorated charges.
- Generate invoice numbers.
- Decide the dunning level from the failed-payment count.
- Build the dunning email for that level.
- Export an accounting journal line.

Why this is a problem:

Proration rules, invoice numbering, payment failure handling, email formatting, and accounting export are different responsibilities. They can change separately.

Refactored into:

- `SubscriptionAccount`
- `ProrationCalculator`
- `InvoiceNumberGenerator`
- `DunningLevel` names the possible collection stages.
- `DunningPolicy` decides the stage from the failed-payment count.
- `DunningEmailFormatter` owns the wording for a supplied stage.
- `LedgerJournalLineExporter`
- `SubscriptionBilling` connects the parts together.

## 8. WarehousePickList

Responsibilities:

- Store the requested warehouse items.
- Calculate how much stock can be allocated.
- Decide the walking order in the warehouse.
- Build the instructions for the picker.
- Detect stock shortages and show shortage warnings.
- Export the pick list as WMS XML.

Why this is a problem:

Stock allocation, warehouse walking order, picker instructions, and XML export are separate responsibilities. Changing the XML format should not affect stock allocation logic.

Refactored into:

- `WarehousePickListState`
- `StockAllocator`
- `WalkingRoutePlanner`
- `StockShortageDetector` identifies shortages using the lab's allocation rules.
- `PickerScriptFormatter` formats the route and the supplied shortage list.
- `WmsXmlExporter`
- `WarehousePickList` connects the parts together.

## 9. GradeBook

Responsibilities:

- Store student scores.
- Calculate student averages.
- Convert the average into a grade.
- Decide if the student is on the honor roll.
- Build the transcript.
- Export grade information as CSV.

Why this is a problem:

Calculating averages, deciding grades, honor-roll rules, transcript formatting, and CSV export are different responsibilities. Each one can change without changing the others.

Refactored into:

- `GradeBookState`
- `GradePolicy`
- `HonorRollPolicy`
- `StudentGradeSummary`
- `GradeSummaryBuilder`
- `TranscriptFormatter`
- `GradeBookCsvExporter`
- `GradeBook` connects the parts together.

## 10. AppointmentDesk

Responsibilities:

- Define the business opening hours.
- Store booked appointments.
- Find the next available appointment.
- Validate and create a booking.
- Generate ICS calendar data.
- Build the SMS reminder.

Why this is a problem:

Opening hours, appointment searching, booking rules, calendar export, and SMS formatting are separate responsibilities. A change in one of them should not affect the others.

Refactored into:

- `BusinessHoursPolicy`
- `AppointmentBook`
- `AppointmentSlotFinder`
- `AppointmentBookingService`
- `IcsCalendarExporter`
- `SmsReminderFormatter`
- `AppointmentDesk` connects the parts together.
