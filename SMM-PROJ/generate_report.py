"""Build SMM_Report.pdf (reportlab). Run from this folder: py generate_report.py"""
import os
from reportlab.lib.pagesizes import A4
from reportlab.lib.units import mm, cm, inch
from reportlab.lib.colors import HexColor, white, black, lightgrey
from reportlab.lib.styles import getSampleStyleSheet, ParagraphStyle
from reportlab.lib.enums import TA_CENTER, TA_LEFT, TA_RIGHT, TA_JUSTIFY
from reportlab.platypus import (SimpleDocTemplate, Paragraph, Spacer, Table,
    TableStyle, PageBreak, Image, Preformatted, KeepTogether,
    NextPageTemplate)
from reportlab.platypus.flowables import HRFlowable
from reportlab.platypus.tableofcontents import TableOfContents
from reportlab.platypus.doctemplate import PageTemplate, BaseDocTemplate, Frame
from reportlab.lib.fonts import addMapping
from reportlab.pdfbase import pdfmetrics
from reportlab.pdfbase.ttfonts import TTFont

BASE_DIR = os.path.dirname(os.path.abspath(__file__))
LOGO_PATH = os.path.join(BASE_DIR, "assets", "nuces_logo.png")
ARCH_PATH = os.path.join(BASE_DIR, "assets", "architecture.png")
OUTPUT_PATH = os.path.join(BASE_DIR, "SMM_Report.pdf")

GITHUB_REPO_URL = "https://github.com/Ninjaa-aa/fast-sms"
GITHUB_REPO_SHORT = "github.com/Ninjaa-aa/fast-sms"

NUCES_BLUE = HexColor("#0072BC")
NUCES_BLUE_DARK = HexColor("#005A94")
LIGHT_BLUE = HexColor("#E8F4FD")
LIGHT_GRAY = HexColor("#F2F2F2")
DARK_GRAY = HexColor("#333333")
MUTED_GRAY = HexColor("#64748B")
COVER_RULE = HexColor("#CBD5E1")

PAGE_W, PAGE_H = A4
MARGIN = 1.8 * cm


# ---------------------------------------------------------------------------
# Styles
# ---------------------------------------------------------------------------

def get_styles():
    base = getSampleStyleSheet()
    styles = {}
    styles["Title"] = ParagraphStyle(
        "Title", parent=base["Title"], fontSize=22,
        leading=26, alignment=TA_CENTER, textColor=NUCES_BLUE,
        fontName="Helvetica-Bold",
    )
    styles["Heading1"] = ParagraphStyle(
        "Heading1", parent=base["Heading1"], fontSize=16,
        leading=20, fontName="Helvetica-Bold", textColor=NUCES_BLUE,
        spaceBefore=20, spaceAfter=10,
    )
    styles["Heading2"] = ParagraphStyle(
        "Heading2", parent=base["Heading2"], fontSize=13,
        leading=16, fontName="Helvetica-Bold", textColor=NUCES_BLUE,
        spaceBefore=14, spaceAfter=6,
    )
    styles["Heading3"] = ParagraphStyle(
        "Heading3", parent=base["Heading3"], fontSize=11,
        leading=14, fontName="Helvetica-Bold", textColor=DARK_GRAY,
        spaceBefore=10, spaceAfter=4,
    )
    styles["BodyText"] = ParagraphStyle(
        "BodyText", parent=base["Normal"], fontSize=10,
        leading=14, alignment=TA_JUSTIFY, fontName="Helvetica",
    )
    styles["BodyBold"] = ParagraphStyle(
        "BodyBold", parent=base["Normal"], fontSize=10,
        leading=14, fontName="Helvetica-Bold",
    )
    styles["SmallBody"] = ParagraphStyle(
        "SmallBody", parent=base["Normal"], fontSize=8,
        leading=10, alignment=TA_JUSTIFY, fontName="Helvetica",
    )
    styles["Code"] = ParagraphStyle(
        "Code", parent=base["Code"], fontSize=7.5,
        leading=9, fontName="Courier", backColor=LIGHT_GRAY,
        leftIndent=6, rightIndent=6, spaceBefore=4, spaceAfter=4,
    )
    styles["TableHeader"] = ParagraphStyle(
        "TableHeader", parent=base["Normal"], fontSize=8,
        leading=10, fontName="Helvetica-Bold", textColor=white,
        alignment=TA_CENTER,
    )
    styles["TableCell"] = ParagraphStyle(
        "TableCell", parent=base["Normal"], fontSize=7,
        leading=9, fontName="Helvetica",
    )
    styles["TableCellSmall"] = ParagraphStyle(
        "TableCellSmall", parent=base["Normal"], fontSize=6.5,
        leading=8, fontName="Helvetica",
    )
    styles["CoverTitle"] = ParagraphStyle(
        "CoverTitle", parent=base["Title"], fontSize=26,
        leading=32, alignment=TA_CENTER, textColor=NUCES_BLUE,
        fontName="Helvetica-Bold",
    )
    styles["CoverInfo"] = ParagraphStyle(
        "CoverInfo", parent=base["Normal"], fontSize=12,
        leading=16, alignment=TA_CENTER, fontName="Helvetica",
    )
    # Cover page — refined hierarchy
    styles["CoverKicker"] = ParagraphStyle(
        "CoverKicker", parent=base["Normal"], fontSize=9,
        leading=12, alignment=TA_CENTER, fontName="Helvetica-Bold",
        textColor=MUTED_GRAY, spaceBefore=0, spaceAfter=0,
    )
    styles["CoverCourseLine"] = ParagraphStyle(
        "CoverCourseLine", parent=base["Normal"], fontSize=11,
        leading=15, alignment=TA_CENTER, fontName="Helvetica",
        textColor=MUTED_GRAY, spaceBefore=0, spaceAfter=0,
    )
    styles["CoverProjectTitle"] = ParagraphStyle(
        "CoverProjectTitle", parent=base["Title"], fontSize=22,
        leading=28, alignment=TA_CENTER, fontName="Helvetica-Bold",
        textColor=NUCES_BLUE, spaceBefore=6, spaceAfter=4,
    )
    styles["CoverProjectTagline"] = ParagraphStyle(
        "CoverProjectTagline", parent=base["Normal"], fontSize=10.5,
        leading=14, alignment=TA_CENTER, fontName="Helvetica-Oblique",
        textColor=MUTED_GRAY, spaceBefore=0, spaceAfter=0,
    )
    styles["CoverSectionLabel"] = ParagraphStyle(
        "CoverSectionLabel", parent=base["Normal"], fontSize=8,
        leading=12, alignment=TA_CENTER, fontName="Helvetica-Bold",
        textColor=MUTED_GRAY, spaceBefore=2, spaceAfter=3,
    )
    styles["CoverSectionLabelSpaced"] = ParagraphStyle(
        "CoverSectionLabelSpaced", parent=base["Normal"], fontSize=8,
        leading=12, alignment=TA_CENTER, fontName="Helvetica-Bold",
        textColor=MUTED_GRAY, spaceBefore=10, spaceAfter=3,
    )
    styles["CoverFacultyName"] = ParagraphStyle(
        "CoverFacultyName", parent=base["Normal"], fontSize=13,
        leading=17, alignment=TA_CENTER, fontName="Helvetica-Bold",
        textColor=DARK_GRAY, spaceBefore=0, spaceAfter=0,
    )
    styles["CoverSectionValue"] = ParagraphStyle(
        "CoverSectionValue", parent=base["Normal"], fontSize=11,
        leading=15, alignment=TA_CENTER, fontName="Helvetica",
        textColor=DARK_GRAY, spaceBefore=0, spaceAfter=0,
    )
    styles["CoverTeamHeader"] = ParagraphStyle(
        "CoverTeamHeader", parent=base["Normal"], fontSize=8,
        leading=11, alignment=TA_CENTER, fontName="Helvetica-Bold",
        textColor=MUTED_GRAY, spaceBefore=18, spaceAfter=8,
    )
    styles["CoverTeamCell"] = ParagraphStyle(
        "CoverTeamCell", parent=base["Normal"], fontSize=10.5,
        leading=14, fontName="Helvetica", textColor=DARK_GRAY,
    )
    styles["CoverTeamCellBold"] = ParagraphStyle(
        "CoverTeamCellBold", parent=base["Normal"], fontSize=10.5,
        leading=14, fontName="Helvetica-Bold", textColor=NUCES_BLUE,
    )
    for lvl, sname in [(0, "Heading1"), (1, "Heading2"), (2, "Heading3")]:
        styles[sname].outlineLevel = lvl

    return styles


# ---------------------------------------------------------------------------
# Helper functions
# ---------------------------------------------------------------------------

_bookmark_seq = [0]


def _next_bookmark():
    _bookmark_seq[0] += 1
    return f"bk{_bookmark_seq[0]}"


def make_table(data, col_widths, styles_dict=None, font_size=7, repeat_header=True):
    if styles_dict is None:
        styles_dict = get_styles()
    header_style = styles_dict["TableHeader"]
    cell_style = ParagraphStyle(
        "_tc", parent=styles_dict["TableCell"], fontSize=font_size,
        leading=font_size + 2,
    )
    table_data = []
    for r_idx, row in enumerate(data):
        new_row = []
        for cell in row:
            st = header_style if r_idx == 0 else cell_style
            new_row.append(Paragraph(str(cell), st))
        table_data.append(new_row)

    tbl = Table(table_data, colWidths=col_widths,
                repeatRows=1 if repeat_header else 0)

    n = len(data)
    cmds = [
        ("BACKGROUND", (0, 0), (-1, 0), NUCES_BLUE),
        ("TEXTCOLOR", (0, 0), (-1, 0), white),
        ("FONTNAME", (0, 0), (-1, 0), "Helvetica-Bold"),
        ("FONTSIZE", (0, 0), (-1, 0), 8),
        ("BOTTOMPADDING", (0, 0), (-1, 0), 4),
        ("TOPPADDING", (0, 0), (-1, 0), 4),
        ("GRID", (0, 0), (-1, -1), 0.4, HexColor("#CCCCCC")),
        ("VALIGN", (0, 0), (-1, -1), "TOP"),
        ("LEFTPADDING", (0, 0), (-1, -1), 3),
        ("RIGHTPADDING", (0, 0), (-1, -1), 3),
        ("TOPPADDING", (0, 1), (-1, -1), 2),
        ("BOTTOMPADDING", (0, 1), (-1, -1), 2),
    ]
    for i in range(1, n):
        bg = LIGHT_GRAY if i % 2 == 0 else white
        cmds.append(("BACKGROUND", (0, i), (-1, i), bg))

    tbl.setStyle(TableStyle(cmds))
    return tbl


def heading(text, level, styles_dict):
    style_map = {1: "Heading1", 2: "Heading2", 3: "Heading3"}
    sname = style_map.get(level, "Heading1")
    st = styles_dict[sname]
    bk = _next_bookmark()
    p = Paragraph(f'<a name="{bk}"/>{text}', st)
    p._bookmarkName = bk
    p._outlineLevel = level - 1
    return p


def para(text, styles_dict, style_name="BodyText"):
    return Paragraph(text, styles_dict[style_name])


def spacer(height=6):
    return Spacer(1, height)


def add_toc_heading(story, text, level, styles_dict):
    h = heading(text, level, styles_dict)
    story.append(h)


# ---------------------------------------------------------------------------
# Document template with TOC support
# ---------------------------------------------------------------------------

class MyDocTemplate(BaseDocTemplate):
    def __init__(self, filename, **kwargs):
        super().__init__(filename, **kwargs)
        content_frame = Frame(
            MARGIN, MARGIN + 0.8 * cm,
            PAGE_W - 2 * MARGIN, PAGE_H - 2 * MARGIN - 1.2 * cm,
            id="normal",
        )
        cover_frame = Frame(
            MARGIN, MARGIN,
            PAGE_W - 2 * MARGIN, PAGE_H - 2 * MARGIN,
            id="cover",
        )
        self.addPageTemplates([
            PageTemplate(id="Cover", frames=[cover_frame],
                         onPage=self._on_cover_page),
            PageTemplate(id="Later", frames=[content_frame],
                         onPage=self._on_later_pages),
        ])

    @staticmethod
    def _on_cover_page(canvas, doc):
        """Full-bleed header band and footer rule (cover only)."""
        canvas.saveState()
        bar_h = 14 * mm
        canvas.setFillColor(NUCES_BLUE_DARK)
        canvas.rect(0, PAGE_H - bar_h, PAGE_W, bar_h, fill=1, stroke=0)
        canvas.setFillColor(NUCES_BLUE)
        canvas.rect(0, PAGE_H - bar_h - 1 * mm, PAGE_W, 1 * mm, fill=1, stroke=0)

        canvas.setFillColor(white)
        canvas.setFont("Helvetica-Bold", 9.5)
        canvas.drawCentredString(
            PAGE_W / 2,
            PAGE_H - bar_h / 2 + 2.2 * mm,
            "NATIONAL UNIVERSITY OF COMPUTER AND EMERGING SCIENCES",
        )
        canvas.setFont("Helvetica", 8)
        canvas.drawCentredString(
            PAGE_W / 2,
            PAGE_H - bar_h / 2 - 2.2 * mm,
            "FAST \u2014 National University of Computer and Emerging Sciences (NUCES)",
        )

        y_rule = 22 * mm
        canvas.setStrokeColor(COVER_RULE)
        canvas.setLineWidth(0.75)
        canvas.line(MARGIN, y_rule, PAGE_W - MARGIN, y_rule)
        canvas.setFillColor(MUTED_GRAY)
        canvas.setFont("Helvetica", 8)
        canvas.drawCentredString(
            PAGE_W / 2,
            y_rule - 4.5 * mm,
            "Software Engineering \u00b7 SE-4011 Software Measurement and Metrics \u00b7 May 2026",
        )
        # Clickable source repository (same URL as body link on cover)
        canvas.setFont("Helvetica", 7.5)
        canvas.setFillColor(NUCES_BLUE)
        tw = canvas.stringWidth(GITHUB_REPO_SHORT, "Helvetica", 7.5)
        x0 = (PAGE_W - tw) / 2
        y_link = y_rule - 9.5 * mm
        canvas.linkURL(
            GITHUB_REPO_URL,
            (x0 - 2, y_link - 1, x0 + tw + 2, y_link + 8),
            relative=0,
        )
        canvas.drawString(x0, y_link, GITHUB_REPO_SHORT)
        canvas.restoreState()

    @staticmethod
    def _on_later_pages(canvas, doc):
        canvas.saveState()
        canvas.setStrokeColor(NUCES_BLUE)
        canvas.setLineWidth(0.8)
        y_top = PAGE_H - MARGIN + 0.2 * cm
        canvas.line(MARGIN, y_top, PAGE_W - MARGIN, y_top)

        canvas.setFont("Helvetica", 7)
        canvas.setFillColor(NUCES_BLUE)
        canvas.drawString(MARGIN, y_top + 4,
                          "FAST Societies Management System \u2014 SE-4011")
        canvas.drawRightString(PAGE_W - MARGIN, y_top + 4,
                               "Software Measurement and Metrics")

        canvas.setFont("Helvetica", 8)
        canvas.setFillColor(DARK_GRAY)
        canvas.drawCentredString(PAGE_W / 2, MARGIN - 0.3 * cm,
                                 f"Page {doc.page}")
        canvas.restoreState()

    def afterFlowable(self, flowable):
        if isinstance(flowable, Paragraph):
            lvl = getattr(flowable, "_outlineLevel", None)
            bk = getattr(flowable, "_bookmarkName", None)
            if lvl is not None and bk is not None:
                txt = flowable.getPlainText()
                key = bk
                self.canv.bookmarkPage(key)
                self.canv.addOutlineEntry(txt, key, level=lvl, closed=False)
                self.notify("TOCEntry", (lvl, txt, self.page, key))


# ---------------------------------------------------------------------------
# Cover page
# ---------------------------------------------------------------------------

def make_cover_team_table(styles):
    """Compact team roster (cover only; distinct from data tables)."""
    avail = PAGE_W - 2 * MARGIN
    inner = min(12.5 * cm, avail * 0.78)
    hdr = styles["CoverTeamCellBold"]
    cell = styles["CoverTeamCell"]
    data = [
        [Paragraph("Name", hdr), Paragraph("Roll number", hdr)],
        [Paragraph("Hammad Zahid", cell), Paragraph("22I-2433", cell)],
        [Paragraph("Abdullah Asif", cell), Paragraph("22I-1527", cell)],
        [Paragraph("Dawood Qammar", cell), Paragraph("22I-2522", cell)],
    ]
    t = Table(data, colWidths=[inner * 0.62, inner * 0.38])
    t.setStyle(TableStyle([
        ("BACKGROUND", (0, 0), (-1, 0), LIGHT_BLUE),
        ("TEXTCOLOR", (0, 0), (-1, 0), NUCES_BLUE_DARK),
        ("LINEBELOW", (0, 0), (-1, 0), 1.0, NUCES_BLUE),
        ("LINEBELOW", (0, 1), (-1, -1), 0.35, COVER_RULE),
        ("ALIGN", (0, 0), (0, -1), "LEFT"),
        ("ALIGN", (1, 0), (1, -1), "CENTER"),
        ("VALIGN", (0, 0), (-1, -1), "MIDDLE"),
        ("TOPPADDING", (0, 0), (-1, -1), 10),
        ("BOTTOMPADDING", (0, 0), (-1, -1), 10),
        ("LEFTPADDING", (0, 0), (-1, -1), 14),
        ("RIGHTPADDING", (0, 0), (-1, -1), 14),
    ]))
    t.hAlign = "CENTER"
    return t


def build_cover(story, styles):
    # Institution line is on the canvas header band; start with logo + title block
    story.append(Spacer(1, 8 * mm))

    if os.path.exists(LOGO_PATH):
        logo = Image(LOGO_PATH, width=3.4 * cm, height=3.4 * cm)
        logo.hAlign = "CENTER"
        story.append(logo)
    else:
        story.append(Paragraph("[Logo not found]", styles["CoverInfo"]))

    story.append(Spacer(1, 10 * mm))
    story.append(Paragraph("COURSE DELIVERABLE", styles["CoverKicker"]))
    story.append(Spacer(1, 3 * mm))

    story.append(HRFlowable(
        width="52%", thickness=1.25, lineCap="butt",
        color=NUCES_BLUE, spaceBefore=2, spaceAfter=10, hAlign="CENTER",
    ))

    story.append(Paragraph(
        "<font name=\"Helvetica-Bold\" color=\"#0072BC\">SE-4011</font>"
        "&nbsp;&nbsp;&nbsp;<font color=\"#64748B\">Software Measurement and "
        "Metrics</font>",
        styles["CoverCourseLine"],
    ))
    story.append(Spacer(1, 14 * mm))
    story.append(Paragraph(
        "FAST Societies Management System",
        styles["CoverProjectTitle"],
    ))
    story.append(Paragraph(
        "Consolidated technical report \u2014 software metrics, reliability, "
        "usability, and estimation analysis",
        styles["CoverProjectTagline"],
    ))

    story.append(HRFlowable(
        width="72%", thickness=0.75, lineCap="butt",
        color=COVER_RULE, spaceBefore=16, spaceAfter=14, hAlign="CENTER",
    ))

    story.append(Paragraph("SUBMITTED TO", styles["CoverSectionLabel"]))
    story.append(Paragraph("Dr. Atif Jillani", styles["CoverFacultyName"]))

    story.append(Paragraph("SECTION", styles["CoverSectionLabelSpaced"]))
    story.append(Paragraph("<b>SE-D</b>", styles["CoverSectionValue"]))

    story.append(Paragraph("DEPARTMENT", styles["CoverSectionLabelSpaced"]))
    story.append(Paragraph(
        "Department of Software Engineering",
        styles["CoverSectionValue"],
    ))

    story.append(Paragraph("GROUP MEMBERS", styles["CoverTeamHeader"]))
    story.append(make_cover_team_table(styles))

    story.append(Spacer(1, 6 * mm))
    story.append(Paragraph(
        '<font size="9" color="#64748B">Source repository: </font>'
        f'<a href="{GITHUB_REPO_URL}" color="#0072BC"><u>{GITHUB_REPO_URL}</u></a>',
        styles["CoverCourseLine"],
    ))

    story.append(NextPageTemplate("Later"))
    story.append(PageBreak())


# ---------------------------------------------------------------------------
# Table of Contents
# ---------------------------------------------------------------------------

def build_toc(story, styles):
    story.append(Paragraph("Table of Contents", styles["Title"]))
    story.append(Spacer(1, 0.8 * cm))

    toc = TableOfContents()
    toc.levelStyles = [
        ParagraphStyle("TOC1", fontName="Helvetica-Bold", fontSize=12,
                       leading=16, leftIndent=20, spaceBefore=6,
                       spaceAfter=2),
        ParagraphStyle("TOC2", fontName="Helvetica", fontSize=10,
                       leading=14, leftIndent=40, spaceBefore=2,
                       spaceAfter=1),
        ParagraphStyle("TOC3", fontName="Helvetica", fontSize=9,
                       leading=12, leftIndent=60, spaceBefore=1,
                       spaceAfter=1),
    ]
    story.append(toc)
    story.append(PageBreak())


# ---------------------------------------------------------------------------
# Task 1 — Project Overview, Architecture, Schema
# ---------------------------------------------------------------------------

def build_task1(story, styles):
    add_toc_heading(story, "1. Project Overview", 1, styles)

    story.append(para(
        "Our university campus has multiple student societies such as Gaming "
        "Society, Sports Society, Developers Club, Literary Society, and Media "
        "Society. These societies organize events, workshops, competitions, "
        "recruitment drives, and awareness campaigns throughout the semester. "
        "The absence of a centralized digital platform for managing student "
        "societies creates communication gaps, inefficient event handling, poor "
        "record management, and lack of administrative oversight.",
        styles))

    story.append(spacer(8))

    # -- 1.1 Functional Requirements --
    add_toc_heading(story, "1.1 Functional Requirements", 2, styles)

    add_toc_heading(story, "1.1.1 Student Functional Requirements", 3, styles)
    student_reqs = [
        "Students shall be able to create accounts and log in securely.",
        "Students shall be able to browse available societies.",
        "Students shall be able to apply for membership in societies.",
        "Students shall be able to join multiple societies.",
        "Students shall be able to view upcoming society events.",
        "Students shall be able to register for events online.",
        "Students shall be able to view their membership status.",
        "Students shall be able to view event tickets/passes.",
    ]
    for i, r in enumerate(student_reqs, 1):
        story.append(para(f"<b>{i}.</b> {r}", styles))
    story.append(spacer(6))

    add_toc_heading(story, "1.1.2 Society Functional Requirements", 3, styles)
    society_reqs = [
        "Society heads shall be able to create and manage society profiles.",
        "Society heads shall be able to approve or reject membership requests.",
        "Society members shall be able to manage internal member lists.",
        "Societies shall be able to create, update, and cancel events.",
        "Societies shall be able to assign tasks to members.",
        "Societies shall be able to generate reports of members and events.",
    ]
    for i, r in enumerate(society_reqs, 1):
        story.append(para(f"<b>{i}.</b> {r}", styles))
    story.append(spacer(6))

    add_toc_heading(story, "1.1.3 Admin Functional Requirements", 3, styles)
    admin_reqs = [
        "Admin shall be able to manage all student accounts.",
        "Admin shall be able to create, approve, suspend, or delete societies.",
        "Admin shall be able to monitor all society activities.",
        "Admin shall be able to approve event requests.",
        "Admin shall be able to generate university-wide reports.",
    ]
    for i, r in enumerate(admin_reqs, 1):
        story.append(para(f"<b>{i}.</b> {r}", styles))
    story.append(spacer(8))

    # -- 1.2 System Architecture --
    add_toc_heading(story, "1.2 System Architecture", 2, styles)
    story.append(para(
        "The FAST Societies Management System follows a layered architecture "
        "with clear separation between the presentation layer (Windows Forms), "
        "the data access layer (DAL), and the database (SQL Server). Helper "
        "and configuration modules support cross-cutting concerns such as "
        "session management, password hashing, and environment-based "
        "configuration.",
        styles))
    story.append(spacer(6))

    if os.path.exists(ARCH_PATH):
        arch_img = Image(ARCH_PATH, width=16 * cm, height=10 * cm,
                         kind="proportional")
        arch_img.hAlign = "CENTER"
        story.append(arch_img)
    else:
        story.append(para("<i>[Architecture diagram not found]</i>", styles))
    story.append(spacer(10))

    # -- 1.3 Database Schema --
    add_toc_heading(story, "1.3 Database Schema (Task 1)", 2, styles)
    story.append(para(
        "The database consists of seven relational tables designed to support "
        "the core operations of the societies management system. Below is the "
        "schema definition followed by a summary table.",
        styles))
    story.append(spacer(4))

    schema_sql = """\
CREATE TABLE Users (
    UserID        INT IDENTITY(1,1) PRIMARY KEY,
    FullName      NVARCHAR(100)  NOT NULL,
    Email         NVARCHAR(100)  NOT NULL UNIQUE,
    PasswordHash  NVARCHAR(255)  NOT NULL,
    Role          NVARCHAR(20)   NOT NULL
        CHECK (Role IN ('Student','SocietyHead','Admin')),
    CreatedAt     DATETIME DEFAULT GETDATE()
);

CREATE TABLE Societies (
    SocietyID    INT IDENTITY(1,1) PRIMARY KEY,
    Name         NVARCHAR(100)  NOT NULL,
    Description  NVARCHAR(500),
    HeadUserID   INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    Status       NVARCHAR(20) DEFAULT 'Pending'
        CHECK (Status IN ('Pending','Active','Suspended')),
    CreatedAt    DATETIME DEFAULT GETDATE()
);

CREATE TABLE Memberships (
    MembershipID INT IDENTITY(1,1) PRIMARY KEY,
    UserID       INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    SocietyID    INT NOT NULL FOREIGN KEY REFERENCES Societies(SocietyID),
    Status       NVARCHAR(20) DEFAULT 'Pending'
        CHECK (Status IN ('Pending','Approved','Rejected')),
    AppliedAt    DATETIME DEFAULT GETDATE()
);

CREATE TABLE Events (
    EventID    INT IDENTITY(1,1) PRIMARY KEY,
    SocietyID  INT NOT NULL FOREIGN KEY REFERENCES Societies(SocietyID),
    Title      NVARCHAR(200)  NOT NULL,
    Description NVARCHAR(1000),
    EventDate  DATETIME       NOT NULL,
    Venue      NVARCHAR(200),
    Status     NVARCHAR(20)  DEFAULT 'Pending'
        CHECK (Status IN ('Pending','Approved','Cancelled')),
    CreatedAt  DATETIME DEFAULT GETDATE()
);

CREATE TABLE EventRegistrations (
    RegistrationID INT IDENTITY(1,1) PRIMARY KEY,
    EventID        INT NOT NULL FOREIGN KEY REFERENCES Events(EventID),
    UserID         INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    TicketCode     NVARCHAR(50) NOT NULL,
    RegisteredAt   DATETIME DEFAULT GETDATE()
);

CREATE TABLE Tasks (
    TaskID      INT IDENTITY(1,1) PRIMARY KEY,
    SocietyID   INT NOT NULL FOREIGN KEY REFERENCES Societies(SocietyID),
    Title       NVARCHAR(200)  NOT NULL,
    Description NVARCHAR(500),
    AssignedTo  INT FOREIGN KEY REFERENCES Users(UserID),
    AssignedBy  INT FOREIGN KEY REFERENCES Users(UserID),
    DueDate     DATETIME,
    Status      NVARCHAR(20) DEFAULT 'Pending'
        CHECK (Status IN ('Pending','Completed')),
    CreatedAt   DATETIME DEFAULT GETDATE()
);

CREATE TABLE Announcements (
    AnnouncementID INT IDENTITY(1,1) PRIMARY KEY,
    SocietyID      INT NOT NULL FOREIGN KEY REFERENCES Societies(SocietyID),
    Title          NVARCHAR(200) NOT NULL,
    Content        NVARCHAR(2000),
    CreatedAt      DATETIME DEFAULT GETDATE()
);"""

    story.append(Preformatted(schema_sql, styles["Code"]))
    story.append(spacer(10))

    schema_summary = [
        ["Table", "Primary Key", "Key Columns", "Relationships"],
        ["Users", "UserID", "FullName, Email, PasswordHash, Role", "\u2014"],
        ["Societies", "SocietyID", "Name, Description, Status",
         "FK: HeadUserID \u2192 Users"],
        ["Memberships", "MembershipID", "Status, AppliedAt",
         "FK: UserID \u2192 Users, SocietyID \u2192 Societies"],
        ["Events", "EventID", "Title, EventDate, Venue, Status",
         "FK: SocietyID \u2192 Societies"],
        ["EventRegistrations", "RegistrationID", "TicketCode",
         "FK: EventID \u2192 Events, UserID \u2192 Users"],
        ["Tasks", "TaskID", "Title, DueDate, Status",
         "FK: SocietyID \u2192 Societies, AssignedTo/By \u2192 Users"],
        ["Announcements", "AnnouncementID", "Title, Content",
         "FK: SocietyID \u2192 Societies"],
    ]
    avail = PAGE_W - 2 * MARGIN
    story.append(make_table(schema_summary,
                            [2.2 * cm, 2.5 * cm, 5.5 * cm, avail - 10.2 * cm],
                            styles))
    story.append(PageBreak())


# ---------------------------------------------------------------------------
# Task 2 — Cyclomatic Complexity & Test Cases
# ---------------------------------------------------------------------------

def build_task2(story, styles):
    add_toc_heading(story, "2. Task 2 \u2014 Cyclomatic Complexity &amp; Test Cases",
                    1, styles)

    story.append(para(
        "Cyclomatic Complexity (CC) measures the number of linearly independent "
        "paths through a method. It is computed as <b>CC = 1 + number of "
        "decision points</b>, where decision points include <i>if</i>, "
        "<i>else if</i>, <i>while</i>, <i>for</i>, <i>foreach</i>, "
        "<i>switch-case</i>, <i>catch</i>, <i>||</i>, <i>&amp;&amp;</i>, "
        "and <i>?:</i> operators. A higher CC indicates more complex logic and "
        "more test cases required for full path coverage.",
        styles))
    story.append(spacer(8))

    tc_style = ParagraphStyle(
        "_tcSmall", parent=styles["TableCellSmall"], fontSize=6, leading=7.5)

    avail = PAGE_W - 2 * MARGIN
    cc_cols = [0.8 * cm, 3.5 * cm, 2.5 * cm, 1.0 * cm,
               avail - 7.8 * cm]

    def _cc_section(title, rows):
        add_toc_heading(story, title, 2, styles)
        header = ["#", "Method", "Decision Points", "CC", "Test Cases"]
        data = [header]
        for row in rows:
            num, method, dp, cc, tc = row
            data.append([str(num), method, dp, str(cc), tc])

        hdr_style = styles["TableHeader"]
        cell_style = styles["TableCell"]
        table_data = []
        for r_idx, row in enumerate(data):
            new_row = []
            for c_idx, cell in enumerate(row):
                if r_idx == 0:
                    new_row.append(Paragraph(str(cell), hdr_style))
                elif c_idx == 4:
                    new_row.append(Paragraph(str(cell), tc_style))
                else:
                    new_row.append(Paragraph(str(cell), cell_style))
            table_data.append(new_row)

        tbl = Table(table_data, colWidths=cc_cols, repeatRows=1)
        n = len(data)
        cmds = [
            ("BACKGROUND", (0, 0), (-1, 0), NUCES_BLUE),
            ("TEXTCOLOR", (0, 0), (-1, 0), white),
            ("FONTNAME", (0, 0), (-1, 0), "Helvetica-Bold"),
            ("FONTSIZE", (0, 0), (-1, 0), 8),
            ("BOTTOMPADDING", (0, 0), (-1, 0), 4),
            ("TOPPADDING", (0, 0), (-1, 0), 4),
            ("GRID", (0, 0), (-1, -1), 0.4, HexColor("#CCCCCC")),
            ("VALIGN", (0, 0), (-1, -1), "TOP"),
            ("LEFTPADDING", (0, 0), (-1, -1), 3),
            ("RIGHTPADDING", (0, 0), (-1, -1), 3),
            ("TOPPADDING", (0, 1), (-1, -1), 2),
            ("BOTTOMPADDING", (0, 1), (-1, -1), 2),
        ]
        for i in range(1, n):
            bg = LIGHT_GRAY if i % 2 == 0 else white
            cmds.append(("BACKGROUND", (0, i), (-1, i), bg))
        tbl.setStyle(TableStyle(cmds))
        story.append(tbl)
        story.append(spacer(8))

    # --- EnvConfig ---
    _cc_section("2.1 EnvConfig", [
        (1, "Load()", "if (1)", 2,
         "TC1: CONNECTION_STRING set. TC2: Empty, builds from components."),
        (2, "EnsureDatabaseInConnectionString()", "|| (1), if (1), ?: (1)", 4,
         "TC1: Has Initial Catalog. TC2: Has Database=. TC3: Missing, ends with ;. TC4: Missing, no ;."),
    ])

    # --- DBHelper ---
    _cc_section("2.2 DBHelper", [
        (3, "GetConnection()", "0", 1,
         "TC1: Valid conn string returns open connection."),
        (4, "ExecuteNonQuery()", "0", 1,
         "TC1: Valid INSERT returns rows &gt; 0."),
        (5, "ExecuteReader()", "0", 1,
         "TC1: Valid SELECT returns DataTable."),
        (6, "ExecuteScalar()", "0", 1,
         "TC1: Valid COUNT returns scalar."),
    ])

    # --- UserDAL ---
    _cc_section("2.3 UserDAL", [
        (7, "GetByEmail()", "if (1)", 2,
         "TC1: Email exists, returns User. TC2: Not found, returns null."),
        (8, "EmailExists()", "0", 1,
         "TC1: Email exists, returns true."),
        (9, "Register()", "0", 1,
         "TC1: Valid User inserts, returns true."),
        (10, "GetAll()", "0", 1,
         "TC1: Returns DataTable with rows."),
        (11, "Search()", "0", 1,
         "TC1: Search term matches rows."),
        (12, "Delete()", "0", 1,
         "TC1: Valid userId deletes row."),
        (13, "GetCount()", "0", 1,
         "TC1: Returns integer count."),
        (14, "GetSocietyHeads()", "0", 1,
         "TC1: Returns SocietyHead users."),
        (15, "MapUser()", "0", 1,
         "TC1: Valid DataRow maps to User."),
    ])

    # --- SocietyDAL ---
    _cc_section("2.4 SocietyDAL", [
        (16, "GetActive()", "0", 1, "TC1: Active societies returned."),
        (17, "GetAll()", "0", 1, "TC1: All societies with head names."),
        (18, "GetByHead()", "0", 1, "TC1: Returns society for head user."),
        (19, "GetActiveCount()", "0", 1, "TC1: Returns active count."),
        (20, "Create()", "0", 1, "TC1: Inserts and returns true."),
        (21, "Approve()", "0", 1, "TC1: Sets status to Active."),
        (22, "Suspend()", "0", 1, "TC1: Sets status to Suspended."),
        (23, "Delete()", "0", 1, "TC1: Deletes society row."),
        (24, "GetPerformanceSummary()", "0", 1,
         "TC1: Returns aggregated stats."),
    ])

    # --- MembershipDAL ---
    _cc_section("2.5 MembershipDAL", [
        (25, "HasApplied()", "0", 1, "TC1: Has membership, returns true."),
        (26, "Apply()", "0", 1, "TC1: Inserts pending row."),
        (27, "GetByUser()", "0", 1, "TC1: Returns joined DataTable."),
        (28, "GetBySociety()", "0", 1, "TC1: Returns society members."),
        (29, "GetBySocietyFiltered()", "0", 1, "TC1: Returns filtered rows."),
        (30, "Approve()", "0", 1, "TC1: Sets status to Approved."),
        (31, "Reject()", "0", 1, "TC1: Sets status to Rejected."),
        (32, "GetApprovedMembers()", "0", 1, "TC1: Returns approved list."),
        (33, "GetAll()", "0", 1, "TC1: Returns full joined table."),
    ])

    # --- EventDAL ---
    _cc_section("2.6 EventDAL", [
        (34, "GetUpcomingApproved()", "0", 1,
         "TC1: Future approved events."),
        (35, "IsRegistered()", "0", 1,
         "TC1: User registered, returns true."),
        (36, "Register()", "0", 1, "TC1: Inserts with ticket code."),
        (37, "GetTicketsByUser()", "0", 1, "TC1: Returns user tickets."),
        (38, "GetBySociety()", "0", 1, "TC1: Returns society events."),
        (39, "Create()", "0", 1, "TC1: Inserts Pending event."),
        (40, "Cancel()", "0", 1, "TC1: Sets status to Cancelled."),
        (41, "GetPending()", "0", 1, "TC1: Returns pending events."),
        (42, "Approve()", "0", 1, "TC1: Sets status to Approved."),
        (43, "Reject()", "0", 1, "TC1: Sets status to Cancelled."),
        (44, "GetPendingCount()", "0", 1, "TC1: Returns pending count."),
        (45, "GetAll()", "0", 1, "TC1: Returns full event table."),
    ])

    # --- TaskDAL ---
    _cc_section("2.7 TaskDAL", [
        (46, "GetBySociety()", "0", 1, "TC1: Returns society tasks."),
        (47, "Create()", "0", 1, "TC1: Inserts Pending task."),
        (48, "MarkComplete()", "0", 1, "TC1: Sets status to Completed."),
    ])

    # --- AnnouncementDAL ---
    _cc_section("2.8 AnnouncementDAL", [
        (49, "GetBySociety()", "0", 1, "TC1: Returns announcements."),
        (50, "Create()", "0", 1, "TC1: Inserts announcement."),
    ])

    # --- Session ---
    _cc_section("2.9 Session", [
        (51, "Clear()", "0", 1, "TC1: All properties reset."),
    ])

    # --- PasswordHasher ---
    _cc_section("2.10 PasswordHasher", [
        (52, "Hash()", "0", 1, "TC1: Returns BCrypt hash string."),
        (53, "Verify()", "0", 1,
         "TC1: Correct password returns true."),
    ])

    # --- Program ---
    _cc_section("2.11 Program", [
        (54, "Main()", "0", 1, "TC1: App starts, LoginForm displayed."),
    ])

    # --- LoginForm ---
    _cc_section("2.12 LoginForm", [
        (55, "LoginForm() ctor", "0", 1, "TC1: Form initialises."),
        (56, "BtnLogin_Click()",
         "if+|| (2), if+|| (2), switch 3 (3), catch (1)", 9,
         "TC1-2: Empty fields. TC3-4: Invalid credentials. TC5-7: Role dispatch. TC8: Unknown role. TC9: DB error."),
        (57, "LnkRegister_LinkClicked()", "0", 1,
         "TC1: Shows RegisterForm."),
    ])

    # --- RegisterForm ---
    _cc_section("2.13 RegisterForm", [
        (58, "RegisterForm() ctor", "0", 1, "TC1: Form initialises."),
        (59, "BtnRegister_Click()",
         "if+||+|| (3), if (1), catch (1), if (1), if (1)", 8,
         "TC1-3: Empty fields. TC4: Password mismatch. TC5: Email exists. TC6: Success. TC7: DB returns false. TC8: DB error."),
        (60, "LnkLogin_LinkClicked()", "0", 1,
         "TC1: Navigates to login."),
        (61, "NavigateToLogin()", "0", 1, "TC1: Creates LoginForm."),
    ])

    # --- StudentDashboard ---
    _cc_section("2.14 StudentDashboard", [
        (62, "StudentDashboard() ctor", "0", 1,
         "TC1: Welcome label set."),
        (63, "BtnBrowseSocieties_Click()", "0", 1,
         "TC1: Shows BrowseSocieties."),
        (64, "BtnMyMemberships_Click()", "0", 1,
         "TC1: Shows MyMemberships."),
        (65, "BtnBrowseEvents_Click()", "0", 1,
         "TC1: Shows BrowseEvents."),
        (66, "BtnMyTickets_Click()", "0", 1,
         "TC1: Shows MyTickets."),
        (67, "BtnLogout_Click()", "0", 1,
         "TC1: Session cleared, logout."),
    ])

    # --- BrowseSocieties ---
    _cc_section("2.15 BrowseSocieties", [
        (68, "BrowseSocieties() ctor", "0", 1,
         "TC1: Form initialises."),
        (69, "BrowseSocieties_Load()", "0", 1,
         "TC1: Calls LoadSocieties."),
        (70, "LoadSocieties()", "catch (1), if (1)", 3,
         "TC1: Loaded, col hidden. TC2: No SocietyID col. TC3: DB error."),
        (71, "BtnApply_Click()", "if (1), catch (1), if (1), if (1)", 5,
         "TC1: No selection. TC2: Already applied. TC3: Success. TC4: Fails. TC5: DB error."),
        (72, "BtnBack_Click()", "0", 1,
         "TC1: Returns to dashboard."),
    ])

    # --- MyMemberships ---
    _cc_section("2.16 MyMemberships", [
        (73, "MyMemberships() ctor", "0", 1, "TC1: Form initialises."),
        (74, "MyMemberships_Load()", "catch (1)", 2,
         "TC1: Grid loads. TC2: DB error."),
        (75, "BtnBack_Click()", "0", 1, "TC1: Returns to dashboard."),
    ])

    # --- BrowseEvents ---
    _cc_section("2.17 BrowseEvents", [
        (76, "BrowseEvents() ctor", "0", 1, "TC1: Form initialises."),
        (77, "BrowseEvents_Load()", "0", 1, "TC1: Calls LoadEvents."),
        (78, "LoadEvents()", "catch (1), if (1)", 3,
         "TC1: Loaded, col hidden. TC2: No EventID col. TC3: DB error."),
        (79, "BtnRegister_Click()", "if (1), catch (1), if (1), if (1)", 5,
         "TC1: No selection. TC2: Already registered. TC3: Success. TC4: Fails. TC5: DB error."),
        (80, "BtnBack_Click()", "0", 1, "TC1: Returns to dashboard."),
    ])

    # --- MyTickets ---
    _cc_section("2.18 MyTickets", [
        (81, "MyTickets() ctor", "0", 1, "TC1: Form initialises."),
        (82, "MyTickets_Load()", "catch (1)", 2,
         "TC1: Grid loads. TC2: DB error."),
        (83, "BtnBack_Click()", "0", 1, "TC1: Returns to dashboard."),
    ])

    # --- SocietyDashboard ---
    _cc_section("2.19 SocietyDashboard", [
        (84, "SocietyDashboard() ctor", "0", 1,
         "TC1: Form initialises."),
        (85, "SocietyDashboard_Load()",
         "catch (1), foreach (1), if (1), if (1)", 5,
         "TC1: Active society found. TC2: Pending only. TC3: Multiple, finds active. TC4: No societies. TC5: DB error."),
        (86, "EnableNavButtons()", "0", 1,
         "TC1: Buttons enabled/disabled."),
        (87, "BtnManageMembers_Click()", "0", 1,
         "TC1: Shows ManageMembers."),
        (88, "BtnManageEvents_Click()", "0", 1,
         "TC1: Shows ManageEvents."),
        (89, "BtnManageTasks_Click()", "0", 1,
         "TC1: Shows ManageTasks."),
        (90, "BtnReports_Click()", "0", 1,
         "TC1: Shows SocietyReports."),
        (91, "BtnLogout_Click()", "0", 1, "TC1: Session cleared."),
    ])

    # --- ManageMembers ---
    _cc_section("2.20 ManageMembers", [
        (92, "ManageMembers() ctor", "0", 1, "TC1: Form initialises."),
        (93, "ManageMembers_Load()", "0", 1,
         "TC1: Calls LoadMembers."),
        (94, "LoadMembers()", "catch (1), if (1), ?: (1), if (1)", 5,
         "TC1: SocietyID null. TC2: Filter All. TC3: Filter Pending. TC4: Col hidden. TC5: DB error."),
        (95, "CmbFilter_SelectedIndexChanged()", "0", 1,
         "TC1: Calls LoadMembers."),
        (96, "BtnApprove_Click()", "if (1), catch (1)", 3,
         "TC1: No selection. TC2: Approved. TC3: DB error."),
        (97, "BtnReject_Click()", "if (1), catch (1)", 3,
         "TC1: No selection. TC2: Rejected. TC3: DB error."),
        (98, "BtnBack_Click()", "0", 1, "TC1: Returns to dashboard."),
    ])

    # --- ManageEvents ---
    _cc_section("2.21 ManageEvents", [
        (99, "ManageEvents() ctor", "0", 1, "TC1: Form initialises."),
        (100, "ManageEvents_Load()", "0", 1, "TC1: Calls LoadEvents."),
        (101, "LoadEvents()", "catch (1), if (1), if (1)", 4,
         "TC1: SocietyID null. TC2: Col hidden. TC3: No col. TC4: DB error."),
        (102, "BtnCreate_Click()", "if (1), catch (1)", 3,
         "TC1: Dialog cancelled. TC2: Created. TC3: DB error."),
        (103, "BtnCancelEvent_Click()", "if (1), if (1), catch (1)", 4,
         "TC1: No selection. TC2: User says No. TC3: Cancelled. TC4: DB error."),
        (104, "BtnBack_Click()", "0", 1, "TC1: Returns to dashboard."),
    ])

    # --- CreateEventDialog ---
    _cc_section("2.22 CreateEventDialog", [
        (105, "CreateEventDialog() ctor", "if+|| (2)", 3,
         "TC1: Valid input. TC2: Empty title. TC3: Empty venue."),
    ])

    # --- ManageTasks ---
    _cc_section("2.23 ManageTasks", [
        (106, "ManageTasks() ctor", "0", 1, "TC1: Form initialises."),
        (107, "ManageTasks_Load()", "0", 1, "TC1: Calls LoadTasks."),
        (108, "LoadTasks()", "catch (1), if (1), if (1)", 4,
         "TC1: SocietyID null. TC2: Col hidden. TC3: No col. TC4: DB error."),
        (109, "BtnAssign_Click()", "if (1), if (1), catch (1)", 4,
         "TC1: SocietyID null. TC2: Dialog cancelled. TC3: Created. TC4: DB error."),
        (110, "BtnComplete_Click()", "if (1), catch (1)", 3,
         "TC1: No selection. TC2: Completed. TC3: DB error."),
        (111, "BtnBack_Click()", "0", 1, "TC1: Returns to dashboard."),
    ])

    # --- AssignTaskDialog ---
    _cc_section("2.24 AssignTaskDialog", [
        (112, "AssignTaskDialog() ctor", "if+|| (2)", 3,
         "TC1: Valid input. TC2: No member. TC3: Empty title."),
    ])

    # --- SocietyReports ---
    _cc_section("2.25 SocietyReports", [
        (113, "SocietyReports() ctor", "0", 1, "TC1: Form initialises."),
        (114, "SocietyReports_Load()", "0", 1,
         "TC1: Calls LoadReports."),
        (115, "LoadReports()", "catch (1), if (1), if (1), if (1)", 5,
         "TC1: SocietyID null. TC2: Both cols hidden. TC3: No UserID col. TC4: No EventID col. TC5: DB error."),
        (116, "BtnRefresh_Click()", "0", 1,
         "TC1: Calls LoadReports."),
        (117, "BtnBack_Click()", "0", 1, "TC1: Returns to dashboard."),
    ])

    # --- AdminDashboard ---
    _cc_section("2.26 AdminDashboard", [
        (118, "AdminDashboard() ctor", "0", 1,
         "TC1: Form initialises."),
        (119, "AdminDashboard_Load()", "catch (1)", 2,
         "TC1: Stats loaded. TC2: DB error."),
        (120, "BtnManageUsers_Click()", "0", 1,
         "TC1: Shows ManageUsers."),
        (121, "BtnManageSocieties_Click()", "0", 1,
         "TC1: Shows ManageSocieties."),
        (122, "BtnApproveEvents_Click()", "0", 1,
         "TC1: Shows ApproveEvents."),
        (123, "BtnReports_Click()", "0", 1,
         "TC1: Shows AdminReports."),
        (124, "BtnLogout_Click()", "0", 1, "TC1: Session cleared."),
    ])

    # --- ManageUsers ---
    _cc_section("2.27 ManageUsers", [
        (125, "ManageUsers() ctor", "0", 1, "TC1: Form initialises."),
        (126, "ManageUsers_Load()", "0", 1, "TC1: Calls LoadUsers."),
        (127, "LoadUsers()", "catch (1), if (1)", 3,
         "TC1: Col hidden. TC2: No col. TC3: DB error."),
        (128, "BtnSearch_Click()", "catch (1), ?: (1), if (1)", 4,
         "TC1: Empty search. TC2: With term. TC3: Col hidden. TC4: DB error."),
        (129, "BtnDelete_Click()", "if (1), if (1), catch (1)", 4,
         "TC1: No selection. TC2: User says No. TC3: Deleted. TC4: DB error."),
        (130, "BtnBack_Click()", "0", 1, "TC1: Returns to dashboard."),
    ])

    # --- ManageSocieties ---
    _cc_section("2.28 ManageSocieties", [
        (131, "ManageSocieties() ctor", "0", 1,
         "TC1: Form initialises."),
        (132, "ManageSocieties_Load()", "0", 1,
         "TC1: Calls LoadSocieties."),
        (133, "LoadSocieties()", "catch (1), if (1)", 3,
         "TC1: Col hidden. TC2: No col. TC3: DB error."),
        (134, "BtnCreate_Click()", "if (1), catch (1)", 3,
         "TC1: Cancelled. TC2: Created. TC3: DB error."),
        (135, "BtnApprove_Click()", "if (1), catch (1)", 3,
         "TC1: No selection. TC2: Approved. TC3: DB error."),
        (136, "BtnSuspend_Click()", "if (1), catch (1)", 3,
         "TC1: No selection. TC2: Suspended. TC3: DB error."),
        (137, "BtnDelete_Click()", "if (1), if (1), catch (1)", 4,
         "TC1: No selection. TC2: User says No. TC3: Deleted. TC4: DB error."),
        (138, "BtnBack_Click()", "0", 1, "TC1: Returns to dashboard."),
    ])

    # --- CreateSocietyDialog ---
    _cc_section("2.29 CreateSocietyDialog", [
        (139, "CreateSocietyDialog() ctor", "if+|| (2)", 3,
         "TC1: Valid input. TC2: Empty name. TC3: No head."),
    ])

    # --- ApproveEvents ---
    _cc_section("2.30 ApproveEvents", [
        (140, "ApproveEvents() ctor", "0", 1,
         "TC1: Form initialises."),
        (141, "ApproveEvents_Load()", "0", 1,
         "TC1: Calls LoadPendingEvents."),
        (142, "LoadPendingEvents()", "catch (1), if (1)", 3,
         "TC1: Col hidden. TC2: No col. TC3: DB error."),
        (143, "BtnApprove_Click()", "if (1), catch (1)", 3,
         "TC1: No selection. TC2: Approved. TC3: DB error."),
        (144, "BtnReject_Click()", "if (1), catch (1)", 3,
         "TC1: No selection. TC2: Rejected. TC3: DB error."),
        (145, "BtnBack_Click()", "0", 1, "TC1: Returns to dashboard."),
    ])

    # --- AdminReports ---
    _cc_section("2.31 AdminReports", [
        (146, "AdminReports() ctor", "0", 1, "TC1: Form initialises."),
        (147, "AdminReports_Load()", "0", 1,
         "TC1: Calls LoadReports."),
        (148, "LoadReports()", "catch (1)", 2,
         "TC1: Reports loaded. TC2: DB error."),
        (149, "BtnRefresh_Click()", "0", 1,
         "TC1: Calls LoadReports."),
        (150, "BtnBack_Click()", "0", 1, "TC1: Returns to dashboard."),
    ])

    # ---- Summary Statistics ----
    story.append(spacer(6))
    add_toc_heading(story, "2.32 Summary Statistics", 2, styles)

    sum_data = [
        ["Metric", "Value"],
        ["Total Methods Analysed", "150"],
        ["Total CC (sum)", "249"],
        ["Average CC", "1.66"],
        ["Median CC", "1"],
        ["Highest CC", "9 \u2014 LoginForm.BtnLogin_Click"],
        ["Second Highest CC", "8 \u2014 RegisterForm.BtnRegister_Click"],
        ["Lowest CC", "1 \u2014 105 methods (70.0%)"],
    ]
    story.append(make_table(sum_data, [5 * cm, avail - 5 * cm], styles,
                            font_size=8))
    story.append(spacer(10))

    # ---- CC Distribution by Module ----
    add_toc_heading(story, "2.33 CC Distribution by Module", 2, styles)
    dist_data = [
        ["Module", "Methods", "Sum CC", "Avg CC"],
        ["Config (EnvConfig)", "2", "6", "3.00"],
        ["DAL (7 classes)", "50", "60", "1.20"],
        ["Helpers", "3", "3", "1.00"],
        ["Entry (Program)", "1", "1", "1.00"],
        ["Forms/Auth", "7", "22", "3.14"],
        ["Forms/Student", "16", "32", "2.00"],
        ["Forms/Society (incl. dialogs)", "34", "72", "2.12"],
        ["Forms/Admin (incl. dialog)", "31", "53", "1.71"],
    ]
    dist_cols = [5.5 * cm, 2 * cm, 2 * cm, 2 * cm]
    story.append(make_table(dist_data, dist_cols, styles, font_size=8))
    story.append(spacer(10))

    # ---- Methods Needing Refactoring ----
    add_toc_heading(story, "2.34 Methods Needing Refactoring (CC \u2265 5)", 2,
                    styles)
    refac_data = [
        ["#", "Class", "Method", "CC", "Suggestion"],
        ["1", "LoginForm", "BtnLogin_Click", "9",
         "Extract role-dispatch; split validation."],
        ["2", "RegisterForm", "BtnRegister_Click", "8",
         "Extract ValidateInput() method."],
        ["3", "BrowseSocieties", "BtnApply_Click", "5",
         'Extract "already applied" check.'],
        ["4", "BrowseEvents", "BtnRegister_Click", "5",
         "Mirrors BrowseSocieties pattern."],
        ["5", "SocietyDashboard", "SocietyDashboard_Load", "5",
         'Extract "find active society" helper.'],
        ["6", "ManageMembers", "LoadMembers", "5",
         "Filter + null check + column hide."],
        ["7", "SocietyReports", "LoadReports", "5",
         "Loads two grids with ID hiding."],
    ]
    refac_cols = [0.7 * cm, 3 * cm, 4 * cm, 1 * cm, avail - 8.7 * cm]
    story.append(make_table(refac_data, refac_cols, styles, font_size=7))
    story.append(spacer(10))

    story.append(para(
        "Overall, the system exhibits low cyclomatic complexity with an average "
        "CC of 1.66 across 150 methods. The majority (70%) of methods have "
        "CC = 1, indicating straightforward, single-path logic. Only 7 methods "
        "exceed the CC \u2265 5 threshold, and even the highest (CC = 9 in "
        "BtnLogin_Click) is well below the critical threshold of 20. The "
        "codebase is maintainable and testable, requiring a minimum of 249 "
        "test cases for full path coverage.",
        styles))
    story.append(PageBreak())


# ---------------------------------------------------------------------------
# Task 3 — Structural Metric: Best Module Justification
# ---------------------------------------------------------------------------

def build_task3(story, styles):
    add_toc_heading(story,
                    "3. Task 3 \u2014 Structural Metric: Best Module Justification",
                    1, styles)

    story.append(para(
        "This section evaluates every class in the system against three "
        "structural metrics \u2014 <b>Lines of Code (LOC)</b>, "
        "<b>Fan-Out</b> (number of distinct external classes/modules "
        "referenced), and <b>Comment Ratio</b> (comment lines / total lines "
        "\u00d7 100). The goal is to identify the <i>best</i> and <i>worst</i> "
        "modules based on a composite assessment of size, coupling, and "
        "documentation quality.",
        styles))
    story.append(spacer(8))

    avail = PAGE_W - 2 * MARGIN

    # ---- Per-class table (37 rows) ----
    add_toc_heading(story, "3.1 Per-Class Structural Metrics", 2, styles)

    struct_header = ["#", "Class", "Module", "LOC", "Fan-Out",
                     "Comment Lines", "Total Lines", "Comment Ratio %"]
    struct_rows = [
        (1, "EnvConfig", "Config", 41, 0, 15, 60, "25.0"),
        (2, "DBHelper", "DAL", 44, 1, 13, 61, "21.3"),
        (3, "UserDAL", "DAL", 91, 2, 22, 123, "17.9"),
        (4, "SocietyDAL", "DAL", 80, 1, 22, 112, "19.6"),
        (5, "MembershipDAL", "DAL", 103, 1, 21, 134, "15.7"),
        (6, "EventDAL", "DAL", 121, 1, 27, 162, "16.7"),
        (7, "TaskDAL", "DAL", 41, 1, 9, 55, "16.4"),
        (8, "AnnouncementDAL", "DAL", 29, 1, 6, 38, "15.8"),
        (9, "Session", "Helpers", 22, 0, 6, 31, "19.4"),
        (10, "PasswordHasher", "Helpers", 15, 0, 6, 24, "25.0"),
        (11, "Program", "Entry", 15, 2, 3, 21, "14.3"),
        (12, "User", "Models", 11, 0, 2, 15, "13.3"),
        (13, "Society", "Models", 11, 0, 2, 15, "13.3"),
        (14, "Event", "Models", 13, 0, 2, 17, "11.8"),
        (15, "Membership", "Models", 11, 0, 2, 15, "13.3"),
        (16, "TaskItem", "Models", 14, 0, 3, 19, "15.8"),
        (17, "Announcement", "Models", 10, 0, 2, 14, "14.3"),
        (18, "LoginForm", "Forms/Auth", 63, 7, 7, 78, "9.0"),
        (19, "RegisterForm", "Forms/Auth", 80, 4, 7, 95, "7.4"),
        (20, "StudentDashboard", "Forms/Student", 41, 6, 4, 51, "7.8"),
        (21, "BrowseSocieties", "Forms/Student", 67, 4, 7, 81, "8.6"),
        (22, "MyMemberships", "Forms/Student", 29, 3, 2, 35, "5.7"),
        (23, "BrowseEvents", "Forms/Student", 67, 3, 4, 78, "5.1"),
        (24, "MyTickets", "Forms/Student", 29, 3, 2, 35, "5.7"),
        (25, "SocietyDashboard", "Forms/Society", 85, 7, 5, 98, "5.1"),
        (26, "ManageMembers", "Forms/Society", 80, 3, 6, 94, "6.4"),
        (27, "ManageEvents", "Forms/Society", 75, 4, 7, 88, "8.0"),
        (28, "CreateEventDialog", "Forms/Society", 53, 0, 2, 58, "3.4"),
        (29, "ManageTasks", "Forms/Society", 74, 4, 6, 86, "7.0"),
        (30, "AssignTaskDialog", "Forms/Society", 57, 1, 2, 63, "3.2"),
        (31, "SocietyReports", "Forms/Society", 51, 4, 2, 58, "3.4"),
        (32, "AdminDashboard", "Forms/Admin", 55, 9, 4, 66, "6.1"),
        (33, "ManageUsers", "Forms/Admin", 75, 2, 6, 88, "6.8"),
        (34, "ManageSocieties", "Forms/Admin", 96, 3, 4, 108, "3.7"),
        (35, "CreateSocietyDialog", "Forms/Admin", 52, 1, 2, 57, "3.5"),
        (36, "ApproveEvents", "Forms/Admin", 68, 2, 6, 81, "7.4"),
        (37, "AdminReports", "Forms/Admin", 39, 4, 2, 46, "4.3"),
    ]
    struct_data = [struct_header]
    for r in struct_rows:
        struct_data.append([str(c) for c in r])

    struct_cols = [0.6 * cm, 2.6 * cm, 2.2 * cm, 1 * cm, 1.2 * cm,
                   1.8 * cm, 1.5 * cm, avail - 10.9 * cm]
    story.append(make_table(struct_data, struct_cols, styles, font_size=6.5))
    story.append(spacer(10))

    # ---- Module-Level Summary ----
    add_toc_heading(story, "3.2 Module-Level Summary", 2, styles)
    mod_header = ["Module", "Classes", "Total LOC", "Avg LOC",
                  "Avg Fan-Out", "Avg Comment Ratio %"]
    mod_rows = [
        ("Config", "1", "41", "41.0", "0.0", "25.0"),
        ("DAL", "7", "509", "72.7", "1.1", "17.6"),
        ("Helpers", "2", "37", "18.5", "0.0", "22.2"),
        ("Entry", "1", "15", "15.0", "2.0", "14.3"),
        ("Models", "6", "70", "11.7", "0.0", "13.6"),
        ("Forms/Auth", "2", "143", "71.5", "5.5", "8.2"),
        ("Forms/Student", "5", "233", "46.6", "3.8", "6.6"),
        ("Forms/Society", "7", "475", "67.9", "3.3", "5.2"),
        ("Forms/Admin", "6", "385", "64.2", "3.5", "5.3"),
    ]
    mod_data = [mod_header] + [list(r) for r in mod_rows]
    mod_cols = [2.5 * cm, 1.3 * cm, 1.6 * cm, 1.5 * cm, 1.8 * cm,
                avail - 8.7 * cm]
    story.append(make_table(mod_data, mod_cols, styles, font_size=7))
    story.append(spacer(12))

    # ---- Best Module ----
    add_toc_heading(story, "3.3 Best Module: PasswordHasher", 2, styles)
    story.append(para(
        "<b>PasswordHasher</b> is identified as the best module in the system "
        "based on the composite structural assessment:", styles))
    story.append(spacer(4))
    best_points = [
        "<b>LOC = 15</b> \u2014 The smallest non-model class, indicating "
        "excellent adherence to the Single Responsibility Principle (SRP). "
        "It does exactly one thing: password hashing and verification.",
        "<b>Fan-Out = 0</b> \u2014 Zero coupling to other project classes. "
        "It depends only on the external BCrypt library, making it fully "
        "self-contained, independently testable, and trivially replaceable.",
        "<b>Comment Ratio = 25.0%</b> \u2014 The highest comment ratio tied "
        "with EnvConfig. One in four lines is a comment, providing excellent "
        "documentation for its cryptographic responsibilities.",
    ]
    for pt in best_points:
        story.append(para(f"\u2022 {pt}", styles))
        story.append(spacer(2))
    story.append(spacer(6))

    # ---- Worst Module ----
    add_toc_heading(story, "3.4 Worst Module: AdminDashboard", 2, styles)
    story.append(para(
        "<b>AdminDashboard</b> is identified as the worst module due to its "
        "extremely high coupling:", styles))
    story.append(spacer(4))
    worst_points = [
        "<b>Fan-Out = 9</b> \u2014 The highest fan-out in the entire system. "
        "It directly references 9 external classes (UserDAL, SocietyDAL, "
        "EventDAL, ManageUsers, ManageSocieties, ApproveEvents, AdminReports, "
        "Session, and MessageBox). Any change in these dependencies risks "
        "breaking AdminDashboard.",
        "<b>Comment Ratio = 6.1%</b> \u2014 Below average documentation for a "
        "class that serves as the central admin navigation hub.",
        "<b>LOC = 55</b> \u2014 While not excessively large, the combination of "
        "high coupling with moderate size indicates a \u201cGod Controller\u201d "
        "anti-pattern where the dashboard orchestrates too many concerns.",
    ]
    for pt in worst_points:
        story.append(para(f"\u2022 {pt}", styles))
        story.append(spacer(2))
    story.append(spacer(8))

    # ---- Conclusion ----
    add_toc_heading(story, "3.5 Conclusion", 2, styles)
    story.append(para(
        "The structural analysis reveals a well-layered system where backend "
        "modules (Config, DAL, Helpers, Models) exhibit low coupling and good "
        "documentation, while frontend Form modules show higher fan-out and "
        "lower comment ratios \u2014 a common pattern in UI-heavy applications. "
        "The DAL layer, despite having the most code (509 LOC across 7 "
        "classes), maintains low average fan-out (1.1) and strong comment "
        "ratios (17.6%), demonstrating disciplined data-access design. "
        "Refactoring efforts should prioritize reducing AdminDashboard\u2019s "
        "fan-out by introducing a mediator or command pattern for navigation.",
        styles))
    story.append(PageBreak())


# ---------------------------------------------------------------------------
# Task 4 — CK Metrics Suite
# ---------------------------------------------------------------------------

def build_task4(story, styles):
    add_toc_heading(story, "4. Task 4 \u2014 CK Metrics Suite", 1, styles)

    story.append(para(
        "The Chidamber &amp; Kemerer (CK) metrics suite provides six "
        "object-oriented design indicators: <b>WMC</b> (Weighted Methods per "
        "Class \u2014 sum of cyclomatic complexities), <b>DIT</b> (Depth of "
        "Inheritance Tree), <b>NOC</b> (Number of Children), <b>CBO</b> "
        "(Coupling Between Objects), <b>RFC</b> (Response For a Class \u2014 "
        "count of methods that can be invoked in response to a message), and "
        "<b>LCOM</b> (Lack of Cohesion in Methods \u2014 number of method "
        "pairs that share no instance variables minus pairs that do, floored "
        "at 0).",
        styles))
    story.append(spacer(8))

    avail = PAGE_W - 2 * MARGIN

    # ---- Full CK table ----
    add_toc_heading(story, "4.1 Complete CK Metrics", 2, styles)

    ck_header = ["#", "Class", "Module", "WMC", "DIT", "NOC",
                 "CBO", "RFC", "LCOM"]
    ck_rows = [
        (1, "EnvConfig", "Config", 6, 0, 0, 2, 7, 1),
        (2, "DBHelper", "DAL", 4, 0, 0, 7, 13, 0),
        (3, "UserDAL", "DAL", 10, 0, 0, 7, 15, 0),
        (4, "SocietyDAL", "DAL", 9, 0, 0, 6, 14, 0),
        (5, "MembershipDAL", "DAL", 9, 0, 0, 6, 14, 0),
        (6, "EventDAL", "DAL", 12, 0, 0, 8, 19, 0),
        (7, "TaskDAL", "DAL", 3, 0, 0, 2, 6, 0),
        (8, "AnnouncementDAL", "DAL", 2, 0, 0, 1, 5, 0),
        (9, "Session", "Helpers", 1, 0, 0, 12, 1, 0),
        (10, "PasswordHasher", "Helpers", 2, 0, 0, 2, 4, 0),
        (11, "Program", "Entry", 1, 0, 0, 2, 5, 0),
        (12, "User", "Models", 0, 0, 0, 2, 0, 0),
        (13, "Society", "Models", 0, 0, 0, 0, 0, 0),
        (14, "Event", "Models", 0, 0, 0, 0, 0, 0),
        (15, "Membership", "Models", 0, 0, 0, 0, 0, 0),
        (16, "TaskItem", "Models", 0, 0, 0, 0, 0, 0),
        (17, "Announcement", "Models", 0, 0, 0, 0, 0, 0),
        (18, "LoginForm", "Forms/Auth", 11, 1, 0, 8, 19, 1),
        (19, "RegisterForm", "Forms/Auth", 11, 1, 0, 4, 15, 3),
        (20, "StudentDashboard", "Forms/Student", 6, 1, 0, 6, 17, 10),
        (21, "BrowseSocieties", "Forms/Student", 11, 1, 0, 4, 16, 4),
        (22, "MyMemberships", "Forms/Student", 4, 1, 0, 3, 10, 1),
        (23, "BrowseEvents", "Forms/Student", 11, 1, 0, 3, 16, 4),
        (24, "MyTickets", "Forms/Student", 4, 1, 0, 3, 10, 1),
        (25, "SocietyDashboard", "Forms/Society", 12, 1, 0, 7, 23, 21),
        (26, "ManageMembers", "Forms/Society", 15, 1, 0, 3, 19, 9),
        (27, "ManageEvents", "Forms/Society", 14, 1, 0, 4, 19, 8),
        (28, "CreateEventDialog", "Forms/Society", 3, 1, 0, 1, 5, 0),
        (29, "ManageTasks", "Forms/Society", 14, 1, 0, 4, 19, 8),
        (30, "AssignTaskDialog", "Forms/Society", 3, 1, 0, 2, 7, 0),
        (31, "SocietyReports", "Forms/Society", 9, 1, 0, 4, 14, 6),
        (32, "AdminDashboard", "Forms/Admin", 8, 1, 0, 9, 21, 15),
        (33, "ManageUsers", "Forms/Admin", 14, 1, 0, 2, 18, 4),
        (34, "ManageSocieties", "Forms/Admin", 19, 1, 0, 3, 21, 9),
        (35, "CreateSocietyDialog", "Forms/Admin", 3, 1, 0, 2, 7, 0),
        (36, "ApproveEvents", "Forms/Admin", 12, 1, 0, 2, 16, 4),
        (37, "AdminReports", "Forms/Admin", 6, 1, 0, 4, 13, 6),
    ]
    ck_data = [ck_header]
    for r in ck_rows:
        ck_data.append([str(c) for c in r])

    ck_cols = [0.6 * cm, 2.8 * cm, 2.2 * cm, 1 * cm, 0.8 * cm, 0.8 * cm,
               1 * cm, 1 * cm, 1 * cm]
    story.append(make_table(ck_data, ck_cols, styles, font_size=6.5))
    story.append(spacer(10))

    # ---- Interpretation Thresholds ----
    add_toc_heading(story, "4.2 Interpretation Thresholds", 2, styles)
    thresh_data = [
        ["Metric", "Low (Good)", "Moderate", "High (Risky)"],
        ["WMC", "\u2264 10", "11\u201320", "&gt; 20"],
        ["DIT", "0\u20131", "2\u20133", "&gt; 3"],
        ["NOC", "0", "1\u20133", "&gt; 3"],
        ["CBO", "\u2264 3", "4\u20137", "&gt; 7"],
        ["RFC", "\u2264 15", "16\u201325", "&gt; 25"],
        ["LCOM", "0", "1\u201310", "&gt; 10"],
    ]
    thresh_cols = [2 * cm, 3 * cm, 3 * cm, 3 * cm]
    story.append(make_table(thresh_data, thresh_cols, styles, font_size=8))
    story.append(spacer(12))

    # ---- Analysis Questions ----
    add_toc_heading(story, "4.3 CK Analysis Questions", 2, styles)

    # Q1
    add_toc_heading(story,
                    "Q1: What is the maximum depth of inheritance?", 3, styles)
    story.append(para(
        "The maximum Depth of Inheritance Tree (DIT) is <b>1</b>. All 20 Form "
        "classes inherit from <i>System.Windows.Forms.Form</i> (DIT = 1), "
        "while the remaining 17 classes (DAL, Models, Helpers, Config, "
        "Program) have DIT = 0, meaning they do not extend any custom class. "
        "This flat hierarchy is a positive indicator \u2014 it minimises the "
        "fragile base-class problem and keeps the system easy to understand.",
        styles))
    story.append(spacer(8))

    # Q2
    add_toc_heading(story,
                    "Q2: Which class has the highest/lowest WMC?", 3, styles)
    story.append(para(
        "The <b>highest WMC is ManageSocieties at 19</b>. It contains 8 "
        "methods handling 4 CRUD operations (Create, Approve, Suspend, "
        "Delete) plus loading and navigation, each with error-handling paths. "
        "The <b>lowest WMC is 0</b>, shared by the 6 model classes (User, "
        "Society, Event, Membership, TaskItem, Announcement) which are pure "
        "data containers with auto-properties and no methods. The next lowest "
        "are Session and Program (WMC = 1 each).",
        styles))
    story.append(spacer(8))

    # Q3
    add_toc_heading(story,
                    "Q3: Which class has the greatest number of children?",
                    3, styles)
    story.append(para(
        "<b>NOC = 0 for every class.</b> No class in the project is subclassed "
        "by another project class. The system uses composition and "
        "form-navigation rather than inheritance hierarchies, resulting in a "
        "completely flat NOC landscape. This is architecturally sound for a "
        "WinForms CRUD application.",
        styles))
    story.append(spacer(8))

    # Q4
    add_toc_heading(story,
                    "Q4: Which class is the most complex?", 3, styles)
    story.append(para(
        "<b>ManageSocieties</b> is the most complex class with WMC = 19 and "
        "RFC = 21. Both metrics are in the moderate-to-high range. Its 8 "
        "methods collectively produce 19 decision paths and can potentially "
        "invoke 21 distinct methods (including calls to SocietyDAL, "
        "UserDAL, MessageBox, and internal helpers). Close runner-ups are "
        "ManageMembers (WMC = 15, RFC = 19) and ManageEvents / ManageTasks "
        "(WMC = 14, RFC = 19 each).",
        styles))
    story.append(spacer(8))

    # Q5
    add_toc_heading(story,
                    "Q5: Which class has the most coupling?", 3, styles)
    story.append(para(
        "<b>Session</b> has the highest CBO at 12, but this is <i>passive</i> "
        "coupling \u2014 it is referenced by 12 other classes as a static "
        "state holder, rather than actively depending on them. Among classes "
        "with <i>active</i> outgoing dependencies, <b>AdminDashboard</b> has "
        "the highest CBO at 9, referencing 9 external classes (3 DAL classes, "
        "4 Form classes, Session, and MessageBox). This high coupling makes "
        "AdminDashboard the most change-sensitive class in the system.",
        styles))
    story.append(spacer(8))

    # Q6
    add_toc_heading(story,
                    "Q6: Which class is the least cohesive?", 3, styles)
    story.append(para(
        "<b>SocietyDashboard</b> has the highest LCOM at 21, making it the "
        "least cohesive class. It contains 8 methods, most of which are "
        "independent navigation button handlers that share no instance "
        "variables with each other. This is characteristic of a "
        "\u201cnavigation hub\u201d form where each button click opens a "
        "different child form. While the high LCOM is expected for this "
        "pattern, it suggests the class could benefit from a command/mediator "
        "pattern to decouple navigation responsibilities. The runner-up is "
        "AdminDashboard (LCOM = 15), which follows the same hub pattern.",
        styles))
    story.append(PageBreak())


# ---------------------------------------------------------------------------
# Task 5 — Fault Injection & Reliability Analysis
# ---------------------------------------------------------------------------

def build_task5(story, styles):
    add_toc_heading(story,
                    "5. Task 5 \u2014 Fault Injection &amp; Reliability Analysis",
                    1, styles)

    story.append(para(
        "For each module, 5 specific faults were injected. Using the Poisson "
        "distribution with \u03bb = 5/(CC+1) and confidence threshold E=1, the "
        "probability P(x &lt;= 1) = e<super>\u2212\u03bb</super> \u00d7 "
        "(1 + \u03bb) was calculated.",
        styles))
    story.append(spacer(6))

    avail = PAGE_W - 2 * MARGIN

    # --- Summary Reliability Table ---
    add_toc_heading(story, "5.1 Reliability Summary (All Modules)", 2, styles)

    rel_header = ["#", "Module", "Faults", "CC (WMC)", "\u03bb",
                  "P(x\u22641)", "Reliability %", "Rank"]
    rel_data = [rel_header,
        ["1", "ManageSocieties", "5", "19", "0.2500", "0.9735", "97.35%", "1"],
        ["2", "ManageMembers", "5", "15", "0.3125", "0.9603", "96.03%", "2"],
        ["3", "ManageEvents", "5", "14", "0.3333", "0.9553", "95.53%", "3"],
        ["4", "ManageTasks", "5", "14", "0.3333", "0.9553", "95.53%", "3"],
        ["5", "ManageUsers", "5", "14", "0.3333", "0.9553", "95.53%", "3"],
        ["6", "EventDAL", "5", "12", "0.3846", "0.9430", "94.30%", "6"],
        ["7", "SocietyDashboard", "5", "12", "0.3846", "0.9430", "94.30%", "6"],
        ["8", "ApproveEvents", "5", "12", "0.3846", "0.9430", "94.30%", "6"],
        ["9", "LoginForm", "5", "11", "0.4167", "0.9338", "93.38%", "9"],
        ["10", "RegisterForm", "5", "11", "0.4167", "0.9338", "93.38%", "9"],
        ["11", "BrowseSocieties", "5", "11", "0.4167", "0.9338", "93.38%", "9"],
        ["12", "BrowseEvents", "5", "11", "0.4167", "0.9338", "93.38%", "9"],
        ["13", "UserDAL", "5", "10", "0.4545", "0.9228", "92.28%", "13"],
        ["14", "SocietyDAL", "5", "9", "0.5000", "0.9098", "90.98%", "14"],
        ["15", "MembershipDAL", "5", "9", "0.5000", "0.9098", "90.98%", "14"],
        ["16", "SocietyReports", "5", "9", "0.5000", "0.9098", "90.98%", "14"],
        ["17", "AdminDashboard", "5", "8", "0.5556", "0.8926", "89.26%", "17"],
        ["18", "EnvConfig", "5", "6", "0.7143", "0.8394", "83.94%", "18"],
        ["19", "StudentDashboard", "5", "6", "0.7143", "0.8394", "83.94%", "18"],
        ["20", "AdminReports", "5", "6", "0.7143", "0.8394", "83.94%", "18"],
        ["21", "DBHelper", "5", "4", "1.0000", "0.7358", "73.58%", "21"],
        ["22", "MyMemberships", "5", "4", "1.0000", "0.7358", "73.58%", "21"],
        ["23", "MyTickets", "5", "4", "1.0000", "0.7358", "73.58%", "21"],
        ["24", "TaskDAL", "5", "3", "1.2500", "0.6446", "64.46%", "24"],
        ["25", "CreateEventDialog", "5", "3", "1.2500", "0.6446", "64.46%", "24"],
        ["26", "AssignTaskDialog", "5", "3", "1.2500", "0.6446", "64.46%", "24"],
        ["27", "CreateSocietyDialog", "5", "3", "1.2500", "0.6446", "64.46%", "24"],
        ["28", "AnnouncementDAL", "5", "2", "1.6667", "0.5037", "50.37%", "28"],
        ["29", "PasswordHasher", "5", "2", "1.6667", "0.5037", "50.37%", "28"],
        ["30", "Session", "5", "1", "2.5000", "0.2873", "28.73%", "30"],
        ["31", "Program", "5", "1", "2.5000", "0.2873", "28.73%", "30"],
    ]
    rel_cols = [0.6*cm, 3*cm, 1*cm, 1.3*cm, 1.2*cm, 1.3*cm, 1.8*cm,
                avail - 10.2*cm]
    story.append(make_table(rel_data, rel_cols, styles, font_size=6.5))
    story.append(spacer(10))

    # --- Representative Per-Module Fault Tables ---
    add_toc_heading(story,
                    "5.2 Representative Fault Injection Details", 2, styles)

    fault_header = ["#", "Fault Type", "Description", "Method"]
    fault_cols = [0.6*cm, 2*cm, avail - 5.2*cm, 2.6*cm]

    def _fault_section(title, rows):
        add_toc_heading(story, title, 3, styles)
        data = [fault_header] + rows
        story.append(make_table(data, fault_cols, styles, font_size=6.5))
        story.append(spacer(6))

    _fault_section("LoginForm (CC=11, Reliability=93.38%)", [
        ["1", "Off-by-one",
         "Changed SelectedRows.Count == 0 guard to &lt; 0",
         "BtnLogin_Click"],
        ["2", "Wrong condition",
         "Changed user == null || !Verify(...) to user != null || ...",
         "BtnLogin_Click"],
        ["3", "Null reference",
         "Removed empty email/password check", "BtnLogin_Click"],
        ["4", "Wrong logic",
         "Set Session.Role = Admin always", "BtnLogin_Click"],
        ["5", "Missing validation",
         "Removed password empty-check", "BtnLogin_Click"],
    ])

    _fault_section("ManageSocieties (CC=19, Reliability=97.35%)", [
        ["1", "Off-by-one",
         "Changed SelectedRows.Count == 0 to &lt; 0", "BtnDelete_Click"],
        ["2", "Wrong condition",
         "Called Suspend() instead of Approve()", "BtnApprove_Click"],
        ["3", "Null reference",
         "Removed row selection check", "BtnSuspend_Click"],
        ["4", "Wrong SQL",
         "Passed UserID instead of SocietyID", "BtnApprove_Click"],
        ["5", "Missing validation",
         "Removed confirmation dialog", "BtnDelete_Click"],
    ])

    _fault_section("DBHelper (CC=4, Reliability=73.58%)", [
        ["1", "Off-by-one",
         "Called connection.Open() twice", "GetConnection"],
        ["2", "Wrong condition",
         "Used SqlCommand(query, null)", "ExecuteNonQuery"],
        ["3", "Null reference",
         "Removed using on connection", "ExecuteReader"],
        ["4", "Wrong SQL",
         "Forgot Parameters.AddRange()", "ExecuteNonQuery"],
        ["5", "Missing validation",
         "Removed null check on ConnectionString", "GetConnection"],
    ])

    _fault_section("Session (CC=1, Reliability=28.73%)", [
        ["1", "Off-by-one",
         "Set UserID = \u22121 instead of 0", "Clear"],
        ["2", "Wrong condition",
         "Left Role unchanged", "Clear"],
        ["3", "Null reference",
         "Set FullName = null instead of empty", "Clear"],
        ["4", "Wrong logic",
         "Only cleared UserID", "Clear"],
        ["5", "Missing validation",
         "Did not set SocietyID = null", "Clear"],
    ])

    _fault_section("BrowseSocieties (CC=11, Reliability=93.38%)", [
        ["1", "Off-by-one",
         "Changed Count == 0 to &lt;= 0", "BtnApply_Click"],
        ["2", "Wrong condition",
         "Negated HasApplied result", "BtnApply_Click"],
        ["3", "Null reference",
         "Removed cell null check", "BtnApply_Click"],
        ["4", "Wrong SQL",
         "Swapped UserID/SocietyID params", "BtnApply_Click"],
        ["5", "Missing validation",
         "Removed selection guard", "BtnApply_Click"],
    ])

    # --- Worked Example ---
    add_toc_heading(story, "5.3 Worked Example \u2014 LoginForm", 2, styles)
    story.append(para(
        "<b>Module:</b> LoginForm &nbsp;|&nbsp; <b>Faults</b> = 5 &nbsp;|&nbsp; "
        "<b>CC (WMC)</b> = 11<br/>"
        "\u03bb = 5 / (11 + 1) = 5/12 = 0.4167<br/>"
        "P(x=0) = e<super>\u22120.4167</super> = 0.6592<br/>"
        "P(x=1) = 0.4167 \u00d7 0.6592 = 0.2746<br/>"
        "P(x \u2264 1) = 0.6592 + 0.2746 = <b>0.9338 = 93.38%</b>",
        styles))
    story.append(spacer(8))

    # --- Most / Least Reliable ---
    add_toc_heading(story, "5.4 Reliability Conclusions", 2, styles)
    story.append(para(
        "<b>Most Reliable: ManageSocieties (97.35%)</b> \u2014 With the highest "
        "cyclomatic complexity (CC=19), this module has the smallest \u03bb "
        "value (0.25). Higher complexity paradoxically yields better reliability "
        "scores because the formula distributes faults across more paths, "
        "reducing the per-path fault density.",
        styles))
    story.append(spacer(4))
    story.append(para(
        "<b>Least Reliable: Session / Program (28.73%)</b> \u2014 These minimal "
        "modules (CC=1) concentrate all 5 faults into a single execution path, "
        "producing \u03bb=2.5. The Poisson probability of at most 1 fault "
        "occurring is only 28.73%, indicating extreme vulnerability to any "
        "injected fault.",
        styles))
    story.append(PageBreak())


# ---------------------------------------------------------------------------
# Task 6 — KLM Usability Evaluation
# ---------------------------------------------------------------------------

def build_task6(story, styles):
    add_toc_heading(story,
                    "6. Task 6 \u2014 KLM Usability Evaluation", 1, styles)

    story.append(para(
        "<b>KLM Operator Reference:</b> K (Keystroke) = 280 ms, "
        "M (Mental preparation) = 1,350 ms, P (Pointing/mouse move) = "
        "1,100 ms, H (Hand switch keyboard\u2194mouse) = 400 ms.",
        styles))
    story.append(spacer(6))

    avail = PAGE_W - 2 * MARGIN

    # --- Summary Table ---
    add_toc_heading(story, "6.1 KLM Task Summary (All Screens)", 2, styles)

    klm_header = ["#", "Screen", "Task", "K", "M", "P", "H",
                  "Total (ms)", "Total (s)"]
    klm_data = [klm_header,
        ["1", "ManageEvents", "Create event", "78", "6", "3", "2",
         "34,040", "34.04"],
        ["2", "ManageTasks", "Assign task", "63", "6", "4", "2",
         "30,940", "30.94"],
        ["3", "RegisterForm", "Register", "54", "6", "4", "2",
         "28,420", "28.42"],
        ["4", "LoginForm", "Log in", "29", "4", "2", "2",
         "16,520", "16.52"],
        ["5", "ManageUsers", "Search+delete", "10", "5", "5", "2",
         "15,850", "15.85"],
        ["6", "BrowseSocieties", "Apply", "0", "2", "2", "0",
         "4,900", "4.90"],
        ["7", "BrowseEvents", "Register", "0", "2", "2", "0",
         "4,900", "4.90"],
        ["8", "ManageMembers", "Approve", "0", "2", "2", "0",
         "4,900", "4.90"],
        ["9", "ManageSocieties", "Approve", "0", "2", "2", "0",
         "4,900", "4.90"],
        ["10", "ApproveEvents", "Approve", "0", "2", "2", "0",
         "4,900", "4.90"],
        ["11", "SocietyReports", "View report", "2", "1", "2", "0",
         "4,110", "4.11"],
        ["12", "AdminReports", "View report", "2", "1", "2", "0",
         "4,110", "4.11"],
        ["13", "MyMemberships", "View status", "2", "1", "1", "0",
         "3,010", "3.01"],
        ["14", "StudentDashboard", "Navigate", "0", "1", "1", "0",
         "2,450", "2.45"],
        ["15", "SocietyDashboard", "Navigate", "0", "1", "1", "0",
         "2,450", "2.45"],
        ["16", "AdminDashboard", "Navigate", "0", "1", "1", "0",
         "2,450", "2.45"],
        ["17", "MyTickets", "View passes", "0", "1", "1", "0",
         "2,450", "2.45"],
    ]
    klm_cols = [0.6*cm, 2.8*cm, 2*cm, 0.8*cm, 0.8*cm, 0.8*cm, 0.8*cm,
                1.8*cm, avail - 10.4*cm]
    story.append(make_table(klm_data, klm_cols, styles, font_size=6.5))
    story.append(spacer(10))

    # --- Step-by-step tables ---
    step_header = ["#", "Action", "Operator", "Time (ms)"]
    step_cols = [0.6*cm, avail - 5.4*cm, 2.4*cm, 2.4*cm]

    # LoginForm
    add_toc_heading(story,
                    "6.2 Step-by-Step: LoginForm (Log in)", 2, styles)
    login_steps = [step_header,
        ["1", "Think about credentials", "M", "1,350"],
        ["2", "Move to Email field", "P", "1,100"],
        ["3", "Switch to keyboard", "H", "400"],
        ["4", "Think before typing email", "M", "1,350"],
        ["5", "Type email (20 chars)", "20K", "5,600"],
        ["6", "Tab to Password", "K", "280"],
        ["7", "Think before typing password", "M", "1,350"],
        ["8", "Type password (8 chars)", "8K", "2,240"],
        ["9", "Switch to mouse", "H", "400"],
        ["10", "Think before Login", "M", "1,350"],
        ["11", "Click Login button", "P", "1,100"],
        ["", "<b>TOTAL</b>", "<b>M=4 H=2 P=2 K=29</b>", "<b>16,520</b>"],
    ]
    story.append(make_table(login_steps, step_cols, styles, font_size=7))
    story.append(spacer(8))

    # RegisterForm
    add_toc_heading(story,
                    "6.3 Step-by-Step: RegisterForm (Register)", 2, styles)
    reg_steps = [step_header,
        ["1", "Think about filling form", "M", "1,350"],
        ["2", "Move to Full Name field", "P", "1,100"],
        ["3", "Switch to keyboard", "H", "400"],
        ["4", "Think before typing name", "M", "1,350"],
        ["5", "Type full name (15 chars)", "15K", "4,200"],
        ["6", "Tab to Email field", "K", "280"],
        ["7", "Think before typing email", "M", "1,350"],
        ["8", "Type email (20 chars)", "20K", "5,600"],
        ["9", "Tab to Password", "K", "280"],
        ["10", "Think before typing password", "M", "1,350"],
        ["11", "Type password (8 chars)", "8K", "2,240"],
        ["12", "Tab to Confirm Password", "K", "280"],
        ["13", "Type confirm password (8 chars)", "8K", "2,240"],
        ["14", "Switch to mouse", "H", "400"],
        ["15", "Think before selecting role", "M", "1,350"],
        ["16", "Move to Role dropdown", "P", "1,100"],
        ["17", "Select Student option", "P", "1,100"],
        ["18", "Think before Register", "M", "1,350"],
        ["19", "Click Register button", "P", "1,100"],
        ["", "<b>TOTAL</b>", "<b>M=6 H=2 P=4 K=54</b>", "<b>28,420</b>"],
    ]
    story.append(make_table(reg_steps, step_cols, styles, font_size=7))
    story.append(spacer(8))

    # ManageEvents
    add_toc_heading(story,
                    "6.4 Step-by-Step: ManageEvents (Create Event)", 2, styles)
    evt_steps = [step_header,
        ["1", "Think about creating event", "M", "1,350"],
        ["2", "Click Create button", "P", "1,100"],
        ["3", "Dialog: Move to Title field", "P", "1,100"],
        ["4", "Switch to keyboard", "H", "400"],
        ["5", "Think before typing title", "M", "1,350"],
        ["6", "Type title (20 chars)", "20K", "5,600"],
        ["7", "Tab to Description field", "K", "280"],
        ["8", "Think before description", "M", "1,350"],
        ["9", "Type description (30 chars)", "30K", "8,400"],
        ["10", "Tab to Date picker", "K", "280"],
        ["11", "Think before date", "M", "1,350"],
        ["12", "Type date (10 chars)", "10K", "2,800"],
        ["13", "Tab to Venue field", "K", "280"],
        ["14", "Think before typing venue", "M", "1,350"],
        ["15", "Type venue (15 chars)", "15K", "4,200"],
        ["16", "Switch to mouse", "H", "400"],
        ["17", "Think before Create", "M", "1,350"],
        ["18", "Click Create button", "P", "1,100"],
        ["", "<b>TOTAL</b>", "<b>K=78 M=6 P=3 H=2</b>", "<b>34,040</b>"],
    ]
    story.append(make_table(evt_steps, step_cols, styles, font_size=7))
    story.append(spacer(10))

    # --- Overall Statistics ---
    add_toc_heading(story, "6.5 Overall KLM Statistics", 2, styles)

    stats_data = [
        ["Metric", "Value"],
        ["Average task time", "9,553 ms (9.55 s)"],
        ["Median task time", "4,900 ms (4.90 s)"],
        ["Total keyboard time", "67,200 ms (240 K)"],
        ["Total mental time", "54,000 ms (40 M)"],
        ["Total pointing time", "46,200 ms (42 P)"],
        ["Total hand-switch time", "4,800 ms (12 H)"],
    ]
    stats_cols = [4*cm, avail - 4*cm]
    story.append(make_table(stats_data, stats_cols, styles, font_size=8))
    story.append(spacer(8))

    # --- Analysis ---
    add_toc_heading(story, "6.6 Performance Analysis", 2, styles)
    story.append(para(
        "<b>Fastest Screens (2,450 ms):</b> Dashboard and navigation screens "
        "(StudentDashboard, SocietyDashboard, AdminDashboard, MyTickets) "
        "require only a single mental preparation and one pointing action. "
        "These are read-only views with minimal interaction.",
        styles))
    story.append(spacer(4))
    story.append(para(
        "<b>Slowest Screen (34,040 ms):</b> ManageEvents requires 78 "
        "keystrokes across multiple form fields using Tab navigation, "
        "6 mental preparations, and 3 pointing actions. The high KLM time "
        "reflects the data-entry-heavy nature of event creation.",
        styles))
    story.append(spacer(4))
    story.append(para(
        "<b>Key Insight:</b> Keyboard input (K) dominates total interaction "
        "time at 67,200 ms (39.0%), followed by mental preparation (M) at "
        "54,000 ms (31.4%). Optimizing form entry with auto-fill, defaults, "
        "and Tab-order improvements would yield the greatest usability gains.",
        styles))
    story.append(PageBreak())


# ---------------------------------------------------------------------------
# Task 7 — COCOMO Model
# ---------------------------------------------------------------------------

def build_task7(story, styles):
    add_toc_heading(story, "7. Task 7 \u2014 COCOMO Model", 1, styles)

    story.append(para(
        "<b>Selected Model:</b> Basic COCOMO &nbsp;|&nbsp; "
        "<b>Mode:</b> Semi-detached<br/>"
        "Semi-detached mode coefficients: a=3.0, b=1.12, c=2.5, d=0.35",
        styles))
    story.append(spacer(6))

    avail = PAGE_W - 2 * MARGIN

    # --- Per-file LOC table ---
    add_toc_heading(story, "7.1 Per-File Lines of Code", 2, styles)

    loc_header = ["#", "File", "Physical LOC", "Blank", "Comments", "Code"]
    loc_data = [loc_header,
        ["1", "EnvConfig.cs", "60", "9", "16", "35"],
        ["2", "DBHelper.cs", "61", "5", "17", "39"],
        ["3", "UserDAL.cs", "123", "14", "29", "80"],
        ["4", "SocietyDAL.cs", "112", "11", "30", "71"],
        ["5", "MembershipDAL.cs", "134", "17", "30", "87"],
        ["6", "EventDAL.cs", "162", "18", "39", "105"],
        ["7", "TaskDAL.cs", "55", "6", "12", "37"],
        ["8", "AnnouncementDAL.cs", "38", "4", "9", "25"],
        ["9", "Session.cs", "31", "2", "10", "19"],
        ["10", "PasswordHasher.cs", "24", "1", "9", "14"],
        ["11", "Program.cs", "21", "2", "4", "15"],
        ["12", "User.cs", "15", "0", "3", "12"],
        ["13", "Society.cs", "15", "0", "3", "12"],
        ["14", "Event.cs", "17", "0", "3", "14"],
        ["15", "Membership.cs", "15", "0", "3", "12"],
        ["16", "TaskItem.cs", "19", "0", "4", "15"],
        ["17", "Announcement.cs", "14", "0", "3", "11"],
        ["18", "LoginForm.cs", "78", "9", "9", "60"],
        ["19", "LoginForm.Designer.cs", "103", "14", "8", "81"],
        ["20", "RegisterForm.cs", "95", "9", "10", "76"],
        ["21", "RegisterForm.Designer.cs", "145", "20", "14", "111"],
        ["22", "StudentDashboard.cs", "51", "6", "6", "39"],
        ["23", "StudentDashboard.Designer.cs", "101", "14", "8", "79"],
        ["24", "BrowseSocieties.cs", "81", "8", "9", "64"],
        ["25", "BrowseSocieties.Designer.cs", "89", "12", "6", "71"],
        ["26", "MyMemberships.cs", "35", "3", "3", "29"],
        ["27", "MyMemberships.Designer.cs", "70", "10", "4", "56"],
        ["28", "BrowseEvents.cs", "78", "8", "6", "64"],
        ["29", "BrowseEvents.Designer.cs", "89", "12", "6", "71"],
        ["30", "MyTickets.cs", "35", "3", "3", "29"],
        ["31", "MyTickets.Designer.cs", "70", "10", "4", "56"],
        ["32", "SocietyDashboard.cs", "98", "10", "7", "81"],
        ["33", "SocietyDashboard.Designer.cs", "111", "15", "9", "87"],
        ["34", "ManageMembers.cs", "94", "12", "9", "73"],
        ["35", "ManageMembers.Designer.cs", "100", "13", "7", "80"],
        ["36", "ManageEvents.cs", "159", "21", "13", "125"],
        ["37", "ManageEvents.Designer.cs", "89", "12", "6", "71"],
        ["38", "ManageTasks.cs", "162", "21", "12", "129"],
        ["39", "ManageTasks.Designer.cs", "89", "12", "6", "71"],
        ["40", "SocietyReports.cs", "58", "7", "3", "48"],
        ["41", "SocietyReports.Designer.cs", "131", "16", "10", "105"],
        ["42", "AdminDashboard.cs", "66", "7", "6", "53"],
        ["43", "AdminDashboard.Designer.cs", "140", "18", "12", "110"],
        ["44", "ManageUsers.cs", "88", "10", "9", "69"],
        ["45", "ManageUsers.Designer.cs", "97", "13", "7", "77"],
        ["46", "ManageSocieties.cs", "177", "20", "9", "148"],
        ["47", "ManageSocieties.Designer.cs", "107", "14", "8", "85"],
        ["48", "ApproveEvents.cs", "81", "8", "9", "64"],
        ["49", "ApproveEvents.Designer.cs", "89", "12", "6", "71"],
        ["50", "AdminReports.cs", "46", "5", "3", "38"],
        ["51", "AdminReports.Designer.cs", "140", "17", "11", "112"],
        ["", "<b>TOTAL</b>", "<b>4,158</b>", "<b>490</b>", "<b>482</b>",
         "<b>3,186</b>"],
    ]
    loc_cols = [0.6*cm, 4.5*cm, 1.8*cm, 1.2*cm, 1.6*cm, avail - 9.7*cm]
    story.append(make_table(loc_data, loc_cols, styles, font_size=6.5))
    story.append(spacer(10))

    # --- LOC Summary ---
    add_toc_heading(story, "7.2 LOC Summary", 2, styles)
    sum_data = [
        ["Metric", "Value"],
        ["Total Files", "51"],
        ["Physical LOC", "4,158"],
        ["Blank Lines", "490"],
        ["Comment Lines", "482"],
        ["Code Lines", "3,186"],
        ["KLOC (Physical)", "4.158"],
        ["KLOC (Logical/Code)", "3.186"],
    ]
    sum_cols = [4*cm, avail - 4*cm]
    story.append(make_table(sum_data, sum_cols, styles, font_size=8))
    story.append(spacer(8))

    # --- Model Justification ---
    add_toc_heading(story, "7.3 Model Justification", 2, styles)
    story.append(para(
        "<b>Semi-detached mode</b> was selected because: (1) the system "
        "involves multiple user roles (Admin, Society Head, Student) with "
        "distinct interfaces; (2) it integrates with a SQL Server database "
        "through a custom DAL layer; (3) the development team has mixed "
        "experience levels with the chosen technology stack (C# WinForms + "
        "SQL Server); and (4) the project size (~4 KLOC) falls within the "
        "semi-detached range (2\u201350 KLOC).",
        styles))
    story.append(spacer(8))

    # --- COCOMO Calculation ---
    add_toc_heading(story, "7.4 COCOMO Calculation", 2, styles)
    cocomo_data = [
        ["Parameter", "Formula", "Calculation", "Result"],
        ["KLOC", "LOC / 1000", "4158 / 1000", "4.158"],
        ["Effort (E)", "3.0 \u00d7 (KLOC)^1.12", "3.0 \u00d7 4.9279",
         "14.78 PM"],
        ["Duration (D)", "2.5 \u00d7 (E)^0.35", "2.5 \u00d7 2.5674",
         "6.42 months"],
        ["Team Size (P)", "E / D", "14.78 / 6.42", "2.30 \u2248 3"],
    ]
    cocomo_cols = [2.5*cm, 3.5*cm, 3.5*cm, avail - 9.5*cm]
    story.append(make_table(cocomo_data, cocomo_cols, styles, font_size=8))
    story.append(spacer(8))

    # --- Estimate vs Actual ---
    add_toc_heading(story, "7.5 Estimate vs. Actual", 2, styles)
    interp_data = [
        ["Metric", "COCOMO Estimate", "Actual"],
        ["Effort", "14.78 Person-Months", "~3 Person-Months"],
        ["Duration", "6.42 months", "~1 month"],
        ["Team Size", "~3 persons", "3 persons"],
    ]
    interp_cols = [3*cm, 4*cm, avail - 7*cm]
    story.append(make_table(interp_data, interp_cols, styles, font_size=8))
    story.append(spacer(8))

    # --- Analysis ---
    add_toc_heading(story, "7.6 Analysis", 2, styles)
    story.append(para(
        "The Basic COCOMO model significantly overestimates both effort "
        "(14.78 vs ~3 PM) and duration (6.42 vs ~1 month). This discrepancy "
        "is attributable to several factors:<br/><br/>"
        "<b>1. AI-Assisted Development:</b> Large portions of the codebase "
        "(especially Designer files and DAL boilerplate) were generated with "
        "AI assistance, dramatically reducing manual coding time.<br/>"
        "<b>2. Template Reuse:</b> WinForms Designer files follow repetitive "
        "patterns; once one form was designed, others were cloned and "
        "adapted.<br/>"
        "<b>3. Model Limitations:</b> Basic COCOMO does not account for "
        "modern development tools, code generation, or framework productivity "
        "multipliers.<br/>"
        "<b>4. Team familiarity grew quickly</b> with the relatively simple "
        "architecture (3-tier, single DB, no distributed components).",
        styles))
    story.append(PageBreak())


# ---------------------------------------------------------------------------
# Task 8 — Documentation Ratio
# ---------------------------------------------------------------------------

def build_task8(story, styles):
    add_toc_heading(story,
                    "8. Task 8 \u2014 Documentation Ratio", 1, styles)

    story.append(para(
        "<b>Formula:</b> Documentation Ratio = Total LOC / Comment Lines "
        "(lower is better).<br/>A lower ratio indicates more comments per "
        "line of code, implying better documentation coverage.",
        styles))
    story.append(spacer(6))

    avail = PAGE_W - 2 * MARGIN

    # --- Per-file table ---
    add_toc_heading(story, "8.1 Per-File Documentation Ratio", 2, styles)

    doc_header = ["#", "File", "Total LOC", "Comments", "Ratio",
                  "Classification"]
    doc_data = [doc_header,
        ["1", "EnvConfig.cs", "60", "16", "3.75", "Well-documented"],
        ["2", "Program.cs", "21", "4", "5.25", "Average"],
        ["3", "DBHelper.cs", "61", "17", "3.59", "Well-documented"],
        ["4", "UserDAL.cs", "123", "29", "4.24", "Well-documented"],
        ["5", "SocietyDAL.cs", "112", "30", "3.73", "Well-documented"],
        ["6", "MembershipDAL.cs", "134", "30", "4.47", "Well-documented"],
        ["7", "EventDAL.cs", "162", "39", "4.15", "Well-documented"],
        ["8", "TaskDAL.cs", "55", "12", "4.58", "Well-documented"],
        ["9", "AnnouncementDAL.cs", "38", "9", "4.22", "Well-documented"],
        ["10", "Session.cs", "31", "10", "3.10", "Well-documented"],
        ["11", "PasswordHasher.cs", "24", "9", "2.67", "Well-documented"],
        ["12", "User.cs", "15", "3", "5.00", "Average"],
        ["13", "Society.cs", "15", "3", "5.00", "Average"],
        ["14", "Event.cs", "17", "3", "5.67", "Average"],
        ["15", "Membership.cs", "15", "3", "5.00", "Average"],
        ["16", "TaskItem.cs", "19", "4", "4.75", "Well-documented"],
        ["17", "Announcement.cs", "14", "3", "4.67", "Well-documented"],
        ["18", "LoginForm.cs", "78", "9", "8.67", "Average"],
        ["19", "RegisterForm.cs", "95", "10", "9.50", "Average"],
        ["20", "StudentDashboard.cs", "51", "6", "8.50", "Average"],
        ["21", "BrowseSocieties.cs", "81", "9", "9.00", "Average"],
        ["22", "MyMemberships.cs", "35", "3", "11.67", "Under-documented"],
        ["23", "BrowseEvents.cs", "78", "6", "13.00", "Under-documented"],
        ["24", "MyTickets.cs", "35", "3", "11.67", "Under-documented"],
        ["25", "SocietyDashboard.cs", "98", "7", "14.00", "Under-documented"],
        ["26", "ManageMembers.cs", "94", "9", "10.44", "Under-documented"],
        ["27", "ManageEvents.cs", "159", "13", "12.23", "Under-documented"],
        ["28", "ManageTasks.cs", "162", "12", "13.50", "Under-documented"],
        ["29", "SocietyReports.cs", "58", "3", "19.33", "Poorly-documented"],
        ["30", "AdminDashboard.cs", "66", "6", "11.00", "Under-documented"],
        ["31", "ManageUsers.cs", "88", "9", "9.78", "Average"],
        ["32", "ManageSocieties.cs", "177", "9", "19.67", "Poorly-documented"],
        ["33", "ApproveEvents.cs", "81", "9", "9.00", "Average"],
        ["34", "AdminReports.cs", "46", "3", "15.33", "Poorly-documented"],
        ["35", "LoginForm.Designer.cs", "103", "8", "12.88",
         "Under-documented"],
        ["36", "RegisterForm.Designer.cs", "145", "14", "10.36",
         "Under-documented"],
        ["37", "StudentDashboard.Designer.cs", "101", "8", "12.63",
         "Under-documented"],
        ["38", "BrowseSocieties.Designer.cs", "89", "6", "14.83",
         "Under-documented"],
        ["39", "MyMemberships.Designer.cs", "70", "4", "17.50",
         "Poorly-documented"],
        ["40", "BrowseEvents.Designer.cs", "89", "6", "14.83",
         "Under-documented"],
        ["41", "MyTickets.Designer.cs", "70", "4", "17.50",
         "Poorly-documented"],
        ["42", "SocietyDashboard.Designer.cs", "111", "9", "12.33",
         "Under-documented"],
        ["43", "ManageMembers.Designer.cs", "100", "7", "14.29",
         "Under-documented"],
        ["44", "ManageEvents.Designer.cs", "89", "6", "14.83",
         "Under-documented"],
        ["45", "ManageTasks.Designer.cs", "89", "6", "14.83",
         "Under-documented"],
        ["46", "SocietyReports.Designer.cs", "131", "10", "13.10",
         "Under-documented"],
        ["47", "AdminDashboard.Designer.cs", "140", "12", "11.67",
         "Under-documented"],
        ["48", "ManageUsers.Designer.cs", "97", "7", "13.86",
         "Under-documented"],
        ["49", "ManageSocieties.Designer.cs", "107", "8", "13.38",
         "Under-documented"],
        ["50", "ApproveEvents.Designer.cs", "89", "6", "14.83",
         "Under-documented"],
        ["51", "AdminReports.Designer.cs", "140", "11", "12.73",
         "Under-documented"],
    ]
    doc_cols = [0.6*cm, 4.5*cm, 1.5*cm, 1.5*cm, 1.2*cm, avail - 9.3*cm]
    story.append(make_table(doc_data, doc_cols, styles, font_size=6.5))
    story.append(spacer(10))

    # --- Project-wide Summary ---
    add_toc_heading(story, "8.2 Project-Wide Summary", 2, styles)
    proj_data = [
        ["Metric", "Value"],
        ["Total Physical LOC", "4,158"],
        ["Total Comment Lines", "482"],
        ["Project Ratio", "8.63"],
        ["Classification", "Average"],
    ]
    proj_cols = [4*cm, avail - 4*cm]
    story.append(make_table(proj_data, proj_cols, styles, font_size=8))
    story.append(spacer(8))

    # --- Classification Distribution ---
    add_toc_heading(story, "8.3 Classification Distribution", 2, styles)
    class_data = [
        ["Classification", "Criteria", "Count", "%"],
        ["Well-documented", "&lt; 5", "12", "23.5%"],
        ["Average", "5 \u2013 10", "11", "21.6%"],
        ["Under-documented", "10 \u2013 15", "23", "45.1%"],
        ["Poorly-documented", "&gt; 15", "5", "9.8%"],
    ]
    class_cols = [3*cm, 2*cm, 1.5*cm, avail - 6.5*cm]
    story.append(make_table(class_data, class_cols, styles, font_size=8))
    story.append(spacer(8))

    # --- Best / Worst Files ---
    add_toc_heading(story,
                    "8.4 Best &amp; Worst Documented Files", 2, styles)
    bw_data = [
        ["Category", "File", "Ratio"],
        ["Best", "PasswordHasher.cs", "2.67"],
        ["2nd best", "Session.cs", "3.10"],
        ["3rd best", "DBHelper.cs", "3.59"],
        ["Worst", "ManageSocieties.cs", "19.67"],
        ["2nd worst", "SocietyReports.cs", "19.33"],
    ]
    bw_cols = [2*cm, 4*cm, avail - 6*cm]
    story.append(make_table(bw_data, bw_cols, styles, font_size=8))
    story.append(spacer(8))

    # --- Module Category Summary ---
    add_toc_heading(story,
                    "8.5 Documentation by Module Category", 2, styles)
    cat_data = [
        ["Category", "Files", "Avg Ratio"],
        ["Config + Entry", "2", "4.50"],
        ["DAL (Data Access)", "7", "4.14"],
        ["Helpers", "2", "2.89"],
        ["Models", "6", "5.02"],
        ["Forms (code-behind)", "17", "12.13"],
        ["Forms (Designer)", "17", "13.89"],
    ]
    cat_cols = [3*cm, 1.5*cm, avail - 4.5*cm]
    story.append(make_table(cat_data, cat_cols, styles, font_size=8))
    story.append(spacer(10))

    # --- Analysis ---
    add_toc_heading(story, "8.6 Vibe-Coding Documentation Analysis", 2,
                    styles)
    analysis_points = [
        "<b>1. Infrastructure code receives the most documentation</b> "
        "\u2014 DAL and helper files (ratios 2.67\u20134.58) contain XML "
        "documentation comments on every public method and class, reflecting "
        "deliberate effort to document the data-access API.",
        "<b>2. UI code-behind has declining documentation quality</b> "
        "\u2014 Form logic files average a ratio of 12.13. Event handlers "
        "and UI wiring are often self-explanatory but lack summary comments "
        "explaining complex workflows.",
        "<b>3. Designer files follow a fixed template</b> \u2014 "
        "Auto-generated Designer.cs files have ratios between 10\u201317.5. "
        "Their comments are boilerplate (#region, component declarations) "
        "and do not reflect intentional documentation effort.",
        "<b>4. Model classes have minimal but adequate documentation</b> "
        "\u2014 With an average ratio of 5.02, model files contain brief "
        "XML summary tags on properties, which is standard practice for "
        "simple POCO/DTO classes.",
        "<b>5. Project-wide ratio of 8.63 (Average)</b> \u2014 This is "
        "acceptable for a vibe-coded university project. The strong "
        "documentation in infrastructure layers compensates for lighter "
        "coverage in UI code, resulting in a balanced overall ratio.",
    ]
    for pt in analysis_points:
        story.append(para(pt, styles))
        story.append(spacer(3))
    story.append(spacer(8))


# ---------------------------------------------------------------------------
# Member contributions (closing section)
# ---------------------------------------------------------------------------

def make_member_contribution_table(styles):
    """Three-column roster: member, roll no., wrapped contribution bullets."""
    avail = PAGE_W - 2 * MARGIN
    w_name = 3.45 * cm
    w_roll = 2.05 * cm
    w_text = avail - w_name - w_roll

    hdr = styles["TableHeader"]
    name_st = ParagraphStyle(
        "_mname", parent=styles["TableCell"], fontSize=10,
        fontName="Helvetica-Bold", textColor=DARK_GRAY, leading=13,
        alignment=TA_LEFT,
    )
    roll_st = ParagraphStyle(
        "_mroll", parent=styles["TableCell"], fontSize=10,
        fontName="Helvetica-Bold", textColor=NUCES_BLUE, leading=13,
        alignment=TA_CENTER,
    )
    cont_st = ParagraphStyle(
        "_mcontrib", parent=styles["TableCell"], fontSize=8.5,
        fontName="Helvetica", leading=11.5, alignment=TA_LEFT,
    )

    def _contrib_cell(items):
        lines = []
        for it in items:
            lines.append(f"\u2022 {it}")
        return Paragraph("<br/>".join(lines), cont_st)

    rows = [
        [
            Paragraph("Member", hdr),
            Paragraph("Roll no.", hdr),
            Paragraph("Primary contributions", hdr),
        ],
        [
            Paragraph("Hammad Zahid", name_st),
            Paragraph("22I-2433", roll_st),
            _contrib_cell([
                "Configuration &amp; session (<font name=\"Courier\" size=\"8\">"
                "EnvConfig</font>, <font name=\"Courier\" size=\"8\">Session"
                "</font>); authentication UI (<font name=\"Courier\" size=\"8\">"
                "LoginForm</font>, <font name=\"Courier\" size=\"8\">"
                "RegisterForm</font>).",
                "Student-role WinForms: societies, events, memberships, "
                "tickets, and dashboard navigation.",
                "Metrics: Task 2 (cyclomatic complexity &amp; test cases), "
                "Task 3 (structural metrics), support for Task 5.",
            ]),
        ],
        [
            Paragraph("Abdullah Asif", name_st),
            Paragraph("22I-1527", roll_st),
            _contrib_cell([
                "Database scripts (<font name=\"Courier\" size=\"8\">schema.sql"
                "</font>, <font name=\"Courier\" size=\"8\">seed.sql</font>); "
                "DAL / <font name=\"Courier\" size=\"8\">DBHelper</font> for "
                "all core entities.",
                "Admin WinForms: dashboards, users, societies, event approvals, "
                "reports.",
                "Metrics: Task 4 (CK suite), Task 5 (fault injection &amp; "
                "Poisson reliability).",
            ]),
        ],
        [
            Paragraph("Dawood Qammar", name_st),
            Paragraph("22I-2522", roll_st),
            _contrib_cell([
                "Society-head WinForms: dashboard, members, events, tasks, "
                "society reports.",
                "Metrics: Task 6 (KLM), Task 7 (COCOMO semi-detached), Task 8 "
                "(documentation ratio).",
                "<font name=\"Courier\" size=\"8\">generate_report.py</font> "
                "PDF pipeline, cover &amp; layout, README and repo hygiene.",
            ]),
        ],
    ]

    tbl = Table(rows, colWidths=[w_name, w_roll, w_text], repeatRows=1)
    tbl.setStyle(TableStyle([
        ("BACKGROUND", (0, 0), (-1, 0), NUCES_BLUE),
        ("TEXTCOLOR", (0, 0), (-1, 0), white),
        ("FONTNAME", (0, 0), (-1, 0), "Helvetica-Bold"),
        ("FONTSIZE", (0, 0), (-1, 0), 9),
        ("BOTTOMPADDING", (0, 0), (-1, 0), 10),
        ("TOPPADDING", (0, 0), (-1, 0), 10),
        ("GRID", (0, 0), (-1, -1), 0.45, HexColor("#CCCCCC")),
        ("VALIGN", (0, 0), (-1, -1), "TOP"),
        ("LEFTPADDING", (0, 0), (-1, -1), 10),
        ("RIGHTPADDING", (0, 0), (-1, -1), 10),
        ("TOPPADDING", (0, 1), (-1, -1), 10),
        ("BOTTOMPADDING", (0, 1), (-1, -1), 10),
        ("ROWBACKGROUNDS", (0, 1), (-1, -1), [white, LIGHT_GRAY]),
    ]))
    return tbl


def build_member_contributions(story, styles):
    story.append(PageBreak())
    add_toc_heading(story, "9. Member Contributions", 1, styles)
    story.append(para(
        "The table below summarizes each member\u2019s main responsibilities "
        "for the FAST Societies Management System implementation, the "
        "software-metrics coursework (Tasks 2\u20138), and the consolidated "
        "PDF report. All members participated in joint design reviews, "
        "integration testing, and final submission checks.",
        styles))
    story.append(spacer(8))

    add_toc_heading(story, "9.1 Contribution summary", 2, styles)
    story.append(spacer(4))

    tbl = make_member_contribution_table(styles)
    story.append(tbl)

    story.append(spacer(14))
    decl_st = ParagraphStyle(
        "_mdecl", parent=styles["SmallBody"], alignment=TA_CENTER,
        fontSize=9, leading=12, spaceBefore=4, textColor=MUTED_GRAY,
    )
    story.append(Paragraph(
        "<i>Declaration:</i> We confirm that the contributions above reflect "
        "our division of work; shared modules were developed collaboratively "
        "with paired reviews where noted.",
        decl_st,
    ))


# ---------------------------------------------------------------------------
# Main
# ---------------------------------------------------------------------------

def main():
    styles = get_styles()

    doc = MyDocTemplate(
        OUTPUT_PATH,
        pagesize=A4,
        leftMargin=MARGIN,
        rightMargin=MARGIN,
        topMargin=MARGIN,
        bottomMargin=MARGIN,
    )

    story = []

    build_cover(story, styles)
    build_toc(story, styles)
    build_task1(story, styles)
    build_task2(story, styles)
    build_task3(story, styles)
    build_task4(story, styles)
    build_task5(story, styles)
    build_task6(story, styles)
    build_task7(story, styles)
    build_task8(story, styles)
    build_member_contributions(story, styles)

    doc.multiBuild(story)
    print(f"Report generated: {OUTPUT_PATH}")


if __name__ == "__main__":
    main()
