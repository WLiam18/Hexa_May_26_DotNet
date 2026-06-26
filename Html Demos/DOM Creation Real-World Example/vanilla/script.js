/* ============================================================
   Departures Board — real-world DOM creation with JavaScript
   Every row on the board is built imperatively with
   document.createElement / textContent / appendChild.
   ============================================================ */

// ---- references to static elements ----
const board   = document.getElementById('board');
const clockEl  = document.getElementById('clock');
const footerEl = document.getElementById('footer');
const legendEl = document.getElementById('legend');

// ---- config / data ----
const FIELDS = ['time', 'flight', 'dest', 'gate', 'status'];

const STATUS_COLOR = {
  'ON TIME':     '#46c878',
  'BOARDING':    '#f3b13e',
  'DELAYED':     '#e85c3a',
  'GATE CLOSED': '#e0473f',
  'DEPARTED':    '#6f6655',
};

const AIRLINES = ['AA', 'UA', 'DL', 'WN', 'AS', 'BA', 'NH', 'AM', 'AC', 'F9'];
const CITIES = ['LOS ANGELES', 'SAN FRANCISCO', 'CHICAGO', 'NEW YORK', 'DENVER',
  'SEATTLE', 'MIAMI', 'BOSTON', 'LONDON', 'TOKYO', 'DALLAS', 'ATLANTA',
  'LAS VEGAS', 'PHOENIX', 'VANCOUVER'];

// our in-memory model — the DOM is rebuilt from this
let flights = [
  { time: '14:05', flight: 'AA 1492', dest: 'LOS ANGELES', gate: 'B12', status: 'BOARDING' },
  { time: '14:20', flight: 'UA 0887', dest: 'CHICAGO',     gate: 'C04', status: 'ON TIME' },
  { time: '14:35', flight: 'DL 0233', dest: 'ATLANTA',     gate: 'A21', status: 'ON TIME' },
  { time: '14:50', flight: 'BA 0286', dest: 'LONDON',      gate: 'D07', status: 'DELAYED' },
  { time: '15:05', flight: 'NH 0175', dest: 'TOKYO',       gate: 'D02', status: 'ON TIME' },
  { time: '15:15', flight: 'WN 0612', dest: 'LAS VEGAS',   gate: 'B09', status: 'GATE CLOSED' },
];

// ============================================================
//  THE CORE: build one row in the DOM
// ============================================================
function createFlightRow(flight) {
  const row = document.createElement('div');     // create the row
  row.className = 'board-row';

  for (const key of FIELDS) {
    const cell = document.createElement('div');   // create each cell
    cell.className = 'flap-cell ' + key;          // e.g. "flap-cell status"
    cell.textContent = flight[key];               // fill with data

    if (key === 'status') {
      cell.style.color = STATUS_COLOR[flight.status] || '#cfc6b5';
      cell.style.textShadow = '0 0 14px ' + (STATUS_COLOR[flight.status] || '#000') + '55';
    }
    row.appendChild(cell);                         // attach cell -> row
  }

  // trigger the split-flap "drop in" animation on the next frame,
  // then drop the class so the row settles in its normal visible state
  requestAnimationFrame(() => row.classList.add('flip-in'));
  row.addEventListener('animationend', () => row.classList.remove('flip-in'));

  return row;                                       // caller mounts it
}

// rebuild the whole board from the model
function renderBoard() {
  board.replaceChildren();                          // clear existing rows
  for (const flight of flights) {
    board.appendChild(createFlightRow(flight));     // create + mount each
  }
  updateFooter();
}

// ============================================================
//  Live updates — flip a single status cell in place
// ============================================================
function setStatus(index, status) {
  flights[index].status = status;

  const row = board.children[index];
  if (!row) return;
  const cell = row.querySelector('.status');
  cell.textContent = status;
  cell.style.color = STATUS_COLOR[status] || '#cfc6b5';
  cell.style.textShadow = '0 0 14px ' + (STATUS_COLOR[status] || '#000') + '55';

  // re-trigger the flap animation
  cell.classList.remove('flap');
  void cell.offsetWidth;          // force reflow so the animation restarts
  cell.classList.add('flap');
}

function nextStatus(s) {
  if (s === 'ON TIME')     return Math.random() < 0.18 ? 'DELAYED' : 'BOARDING';
  if (s === 'DELAYED')     return 'BOARDING';
  if (s === 'BOARDING')    return 'GATE CLOSED';
  if (s === 'GATE CLOSED') return 'DEPARTED';
  return 'DEPARTED';
}

// every few seconds, advance a random flight's status
function tick() {
  const open = flights
    .map((f, i) => i)
    .filter(i => flights[i].status !== 'DEPARTED');
  if (!open.length) return;
  const i = open[Math.floor(Math.random() * open.length)];
  setStatus(i, nextStatus(flights[i].status));
  updateFooter();
}

// ============================================================
//  Controls
// ============================================================
function pad(n, len) { return String(n).padStart(len, '0'); }
function pick(arr)   { return arr[Math.floor(Math.random() * arr.length)]; }

function addFlight() {
  const base = new Date(Date.now() + (15 + Math.floor(Math.random() * 120)) * 60000);
  const flight = {
    time:   pad(base.getHours(), 2) + ':' + pad(base.getMinutes(), 2),
    flight: pick(AIRLINES) + ' ' + pad(Math.floor(Math.random() * 9000) + 100, 4),
    dest:   pick(CITIES),
    gate:   'ABCD'[Math.floor(Math.random() * 4)] + pad(Math.floor(Math.random() * 24) + 1, 2),
    status: 'ON TIME',
  };

  flights.push(flight);
  if (flights.length > 9) flights.shift();   // keep the board to 9 rows

  // only create the NEW row, rather than rebuilding everything
  if (flights.length <= 9 && board.children.length === flights.length - 1) {
    board.appendChild(createFlightRow(flight));
  } else {
    renderBoard();
  }
  updateFooter();
}

function reset() {
  flights = [
    { time: '14:05', flight: 'AA 1492', dest: 'LOS ANGELES', gate: 'B12', status: 'BOARDING' },
    { time: '14:20', flight: 'UA 0887', dest: 'CHICAGO',     gate: 'C04', status: 'ON TIME' },
    { time: '14:35', flight: 'DL 0233', dest: 'ATLANTA',     gate: 'A21', status: 'ON TIME' },
    { time: '14:50', flight: 'BA 0286', dest: 'LONDON',      gate: 'D07', status: 'DELAYED' },
    { time: '15:05', flight: 'NH 0175', dest: 'TOKYO',       gate: 'D02', status: 'ON TIME' },
    { time: '15:15', flight: 'WN 0612', dest: 'LAS VEGAS',   gate: 'B09', status: 'GATE CLOSED' },
  ];
  renderBoard();
}

function updateFooter() {
  const boarding = flights.filter(f => f.status === 'BOARDING').length;
  const delayed  = flights.filter(f => f.status === 'DELAYED').length;
  footerEl.textContent =
    `${flights.length} departures · ${boarding} boarding · ${delayed} delayed`;
}

// ============================================================
//  Clock + legend
// ============================================================
function updateClock() {
  const d = new Date();
  clockEl.textContent =
    pad(d.getHours(), 2) + ':' + pad(d.getMinutes(), 2) + ':' + pad(d.getSeconds(), 2);
}

function buildLegend() {
  for (const status of Object.keys(STATUS_COLOR)) {
    const item = document.createElement('div');
    item.className = 'legend-item';

    const swatch = document.createElement('span');
    swatch.className = 'swatch';
    swatch.style.background = STATUS_COLOR[status];

    const label = document.createElement('span');
    label.className = 'label';
    label.textContent = status;

    item.append(swatch, label);   // attach both children at once
    legendEl.appendChild(item);
  }
}

// ============================================================
//  Boot
// ============================================================
document.getElementById('addBtn').addEventListener('click', addFlight);
document.getElementById('resetBtn').addEventListener('click', reset);

buildLegend();
renderBoard();
updateClock();
setInterval(updateClock, 1000);   // live clock
setInterval(tick, 3400);          // live status engine
